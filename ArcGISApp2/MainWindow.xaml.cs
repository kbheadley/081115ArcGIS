
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;  // for colors


using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Tasks.Query;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Http;
using Esri.ArcGISRuntime.Tasks.Offline;
using Esri.ArcGISRuntime.Portal;


namespace ArcGISApp2
{
    public partial class MainWindow : Window
    {
        private string featureLayerServiceUrl = string.Empty;
        private string basemapLayerServiceUrl = string.Empty;
     
      
        public MainWindow()
        {
            InitializeComponent();
            //GetLayerUrls(); //doesn't work
            //_graphicsOverlay = MyMapView.GraphicsOverlays["graphicsOverlay"];
            //GetWebMap();
            AddGraphics("540 East Main Street, Riverhead, NY  11901", "Alternatives Counseling Services, Inc.");
            AddGraphics("3281 Veterans Memorial Highway, Ronkonkoma NY 11779", "Community Counseling Services of Ronkonkoma");
            AddGraphics("423 Park Avenue Huntington NY","Huntington Youth Bureau");
            AddGraphics("2760 Middle Country Road Lake Grove NY", "IMPACT Counseling Services");
            AddGraphics("30 Floyds Run Bohemia NY", "Institute for Rational Counseling");
            AddGraphics("208 Route 112 Port Jefferson NY","John T Mather Memorial Hospital");
            AddGraphics("300 Motor Parkway Hauppauge NY","The Kenneth Peters Center for Recovery");

        }
        private  void GetLocalMap()
        {
           // MessageBox.Show("Unable to access item ");
          //var varOldBaseMap = MyMap
            //MyMapView.Map.Layers.Add(new ArcGISTiledMapServiceLayer()
            //{
            //    ServiceUri = ("http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer")
            //});
        }
        private async void GetWebMap()
        {
         // get a reference to the portal (ArcGIS Online)
            var arcGISOnline = await ArcGISPortal.CreateAsync();

            // get the id of the web map to load
          
            var webMapId = "777e6386cf8a4390909325e90c3033ae";
            
            ArcGISPortalItem webMapItem = null;

            // get a portal item using its ID (ArcGISWebException is thrown if the item is not found)
            try
            {
                webMapItem = await ArcGISPortalItem.CreateAsync(arcGISOnline, webMapId);
            }
            catch (ArcGISWebException exp)
            {
                MessageBox.Show("Unable to access item '" + webMapId + "'.");
                return;
            }

            // check type: if the item is not a web map, return
            if (webMapItem.Type != Esri.ArcGISRuntime.Portal.ItemType.WebMap) { return; }

            // get the web map represented by the portal item
            var webMap = await Esri.ArcGISRuntime.WebMap.WebMap.FromPortalItemAsync(webMapItem);

            // create a WebMapViewModel and load the web map
            var webMapVM = await Esri.ArcGISRuntime.WebMap.WebMapViewModel.LoadAsync(webMap, arcGISOnline);
            

            // get the map from the view model; display it in the app's map view
            this.MyMapView.Map = webMapVM.Map;
            var foo = webMap.OperationalLayers[0].ShowLegend;
}

        private async void AddGraphics(string p_parameters, string p_textLabel )
        {
            //The constructor takes two arguments: the URI for the geocode service and a token (required for secured services).
            var uri = new Uri("http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer");
            var token = String.Empty;
            var locator = new OnlineLocatorTask(uri, token);

            // OnlineLocatorFindParameters object, which holds all relevant information for the address search.
            var findParams = new OnlineLocatorFindParameters(p_parameters);
            findParams.OutSpatialReference = MyMapView.SpatialReference;
            findParams.SourceCountry = "US";

            // 
            var results = await locator.FindAsync(findParams, new System.Threading.CancellationToken());

            if (results.Count > 0)
            {
                var firstMatch = results[0].Feature;
                var matchLocation = firstMatch.Geometry as MapPoint;

                //Add a point graphic at the address location
                var matchSym = new PictureMarkerSymbol();
                var pictureUri = new Uri("http://static.arcgis.com/images/Symbols/Animated/EnlargeGradientSymbol.png");
                await matchSym.SetSourceAsync(pictureUri);

                var matchGraphic = new Graphic(matchLocation, matchSym);

                // Get a reference to the graphic layer you defined for the map, and add the new graphic.
                var graphicsLayer = MyMap.Layers["MyGraphics"] as GraphicsLayer;
                graphicsLayer.Graphics.Add(matchGraphic);

                // create a text symbol: define color, font, size, and text for the label
                var textSym = new Esri.ArcGISRuntime.Symbology.TextSymbol();
                textSym.Color = Colors.Red;
                textSym.Font = new Esri.ArcGISRuntime.Symbology.SymbolFont("Arial", 12);
                textSym.BackgroundColor = Colors.SeaShell;
                textSym.Text = p_textLabel;
                //textSym.Angle = -60;
               // create a graphic for the text (apply TextSymbol)
                var textGraphic = new Esri.ArcGISRuntime.Layers.Graphic(matchLocation, textSym);
                graphicsLayer.Graphics.Add(textGraphic);


            }
        }

               private async void GetLayerUrls()
        {
            await MyMapView.LayersLoadedAsync();

            var basemapLayer = MyMapView.Map.Layers["BaseMap"] as ArcGISTiledMapServiceLayer;
            if (basemapLayer != null) { this.basemapLayerServiceUrl = basemapLayer.ServiceUri; }

            var featureLayer = MyMapView.Map.Layers["2014SubstanceAbuse"] as FeatureLayer;
            var table = featureLayer.FeatureTable as ServiceFeatureTable;
            if (table != null) { this.featureLayerServiceUrl = table.ServiceUri; }

        }

        private void MyMapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var sel = combo.SelectedItem as ComboBoxItem;
            if (sel.Tag == null) { return; }

            // Find and remove the current basemap layer from the map
            if (MyMap == null) { return; }
            var oldBasemap = MyMap.Layers["BaseMap"];
            MyMap.Layers.Remove(oldBasemap);
            

            // Create a new basemap layer
            var newBasemap = new Esri.ArcGISRuntime.Layers.ArcGISTiledMapServiceLayer();

            // Set the ServiceUri with the url defined for the ComboBoxItem's Tag
            newBasemap.ServiceUri = sel.Tag.ToString();

            // Give the layer the same ID so it can still be found with the code above
            newBasemap.ID = "BaseMap";

            // Insert the new basemap layer as the first (bottom) layer in the map
            MyMap.Layers.Insert(0, newBasemap);
        }

        private async void GetParcelAddressButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the mappoint clicked by the User
            var mapPoint = await MyMapView.Editor.RequestPointAsync();
            // find feature at the location specified by the user:
            var poolPermitUrl = "http://sampleserver6.arcgisonline.com/arcgis/rest/services/PoolPermits/FeatureServer/0";
            var queryTask = new QueryTask(new System.Uri(poolPermitUrl));
            var queryFilter = new Query(mapPoint);
            queryFilter.OutFields.Add("apn");
            queryFilter.OutFields.Add("address");

            // If a result is found, its APN and address values are displayed in the UI.
            // NOTE:  Zoom in to a part of the map displaying pool permit features. 
            //Click the Get Parcel Info button, then click one of the features in the map
            //to display its APN and address values. Click the button again,
            //    and try a neighboring parcel that is not currently displayed in the map. 
            var queryResult = await queryTask.ExecuteAsync(queryFilter);
            if (queryResult.FeatureSet.Features.Count > 0)
            {
                var resultGraphic = queryResult.FeatureSet.Features[0] as Graphic;
                //ApnTextBlock.Text = resultGraphic.Attributes["apn"].ToString();
                //AddressTextBlock.Text = resultGraphic.Attributes["address"].ToString();
            }
        }

        private async void FindAddressButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                progress.Visibility = Visibility.Visible;

            //The constructor takes two arguments: the URI for the geocode service and a token (required for secured services).
            var uri = new Uri("http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer");
            var token = String.Empty;
            var locator = new OnlineLocatorTask(uri, token);

            // OnlineLocatorFindParameters object, which holds all relevant information for the address search.
            //var findParams = new OnlineLocatorFindParameters(AddressTextBox.Text);
            var findParams = new OnlineLocatorFindParameters(InputAddress.Text + " " + City.Text + " " + State.Text + " " + Zip.Text);
            findParams.OutSpatialReference = MyMapView.SpatialReference;
            findParams.SourceCountry = "US";

            // 
            
            var results = await locator.FindAsync(findParams, new System.Threading.CancellationToken());

            if (results.Count > 0)
            {
                var firstMatch = results[0].Feature;
                var matchLocation = firstMatch.Geometry as MapPoint;

                //Add a point graphic at the address location
                var matchSym = new PictureMarkerSymbol();
                var pictureUri = new Uri("http://static.arcgis.com/images/Symbols/Basic/GreenStickpin.png");
                await matchSym.SetSourceAsync(pictureUri);

                var matchGraphic = new Graphic(matchLocation, matchSym);

                // Get a reference to the graphic layer you defined for the map, and add the new graphic.
                var graphicsLayer = MyMap.Layers["MyGraphics"] as GraphicsLayer;
                graphicsLayer.Graphics.Add(matchGraphic);
               // _graphicsOverlay.Graphics.Add(matchGraphic);

                txtResult.Visibility = System.Windows.Visibility.Visible;
                txtResult.Text = ("Address Found:  " +  matchLocation.X +",  " +  matchLocation.Y);

                // zooms into pin point graphic:
                //The Envelope is created by subtracting 1000 meters from the location's
                //minimum X and Y values and adding 1000 meters to the maximum X and Y values.

                var matchExtent = new Envelope(matchLocation.X,
                               matchLocation.Y,
                               matchLocation.X,
                               matchLocation.Y);
                await MyMapView.SetViewAsync(matchExtent);

            }
            else
            {
                MessageBox.Show("Unable to find address. ");
                return;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find address. ", "");
            }
        }

        private void LoadWebMap_Click(object sender, RoutedEventArgs e)
        {
            GetWebMap();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadLocalMap_Click(object sender, RoutedEventArgs e)
        {
            GetLocalMap();
        }
    }
}
