
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
using Esri.ArcGISRuntime.Tasks.NetworkAnalyst;


namespace ArcGISApp2
{
    public partial class MainWindow : Window
    {
        private string featureLayerServiceUrl = string.Empty;
        private string basemapLayerServiceUrl = string.Empty;

        public Graphic oldGraphic ;
        public int intFoo;

      
        public MainWindow()
        {
            InitializeComponent();
            //GetLayerUrls(); //doesn't work
            //_graphicsOverlay = MyMapView.GraphicsOverlays["graphicsOverlay"];
            //GetWebMap();
            AddGraphics("540 East Main Street, Riverhead, NY  11901", "Alternatives Counseling Services, Inc.");
            Delay();

            AddGraphics("291 Hampton Road Southampton NY","Alternatives Counseling Services, Inc.");
            Delay();
            AddGraphics("770 Grand Blvd. Suite 17,Deer Park NY", "Behavioral Enhancement & Substance Abuse Medicine Treatment B.E.S.T");
            Delay();
            AddGraphics("365 East Main Street Patchogue NY", "Brookhaven Memorial Hospital/Medical Center");
            Delay();
            AddGraphics("550 Montauk Hwy Shirley NY", "Brookhaven Memorial Hospital/Medical Center");
            Delay();
            AddGraphics("30-C Carlough Road Bohemia NY", "Catholic Charities of Rockville Centre Talbot House-Crisis Center");
            Delay();
            AddGraphics("155 Indian Head Road Commack NY", "Catholic Charities of Rockville Centre");
            Delay();
            AddGraphics("31 Montauk Highway East Hampton Bays NY", "Catholic Charities of Rockville Centre");
            Delay();
            AddGraphics("3001 Expressway Drive North Islandia NY", "Center for Addiction Recovery And Empowerment");
            Delay();
            AddGraphics("998 Crooked Hill Rd West Brentwood NY", "Charles K. Post Addiction Treatment");
            Delay();
            AddGraphics("3281 Veterans Memorial Highway Ronkonkoma NY", "Community Counseling Services of Ronkonkoma" );
            Delay();
            AddGraphics("2075 New York Avenue Huntington Station NY", "Daytop Village Inc.");
            Delay();
            AddGraphics("26 Bull Run East Hampton NY", "The Dunes East Hampton");
            Delay();
            AddGraphics("201 Manor Place Greenport NY", "Eastern L.I. Hospital Association");
            Delay();
            AddGraphics("814 Harrison Avenue Riverhead NY", "Eastern L.I. Hospital Association");
            Delay();
            AddGraphics("1741D No. Ocean Ave Medford NY", "Eastern Suffolk BOCES Student Assistance Service");
            Delay();
            AddGraphics("278 East Main Street Smithtown NY", "Employee Assistance Resource Services");
            Delay();
            AddGraphics("464 William Floyd Pkwy Shirley NY", "Family Counseling Services");
            Delay();
            AddGraphics("40 Main Street Westhampton Beach NY", "Family Counseling Services");
            Delay();
            AddGraphics("208 Roanoke Avenue Riverhead NY", "Family Service League, Inc.");
            Delay();
            AddGraphics("1444 Fifth Avenue Bayshore NY", "Family Service League, Inc.");
            Delay();
            AddGraphics("66 Newton Lane East Hampton NY", "Family Service League");
            Delay();
            AddGraphics("1490 William Floyd Parkway East Yaphank NY", "Family Service League");
            Delay();
            AddGraphics("201 Dixon Avenue Amityville NY", "Hope For Youth, Inc.");
            Delay();
            AddGraphics("110 Mill Road Westhampton Beach NY", "Human Understanding & Growth Seminars, Inc. (HUGS)");
            Delay();
            //AddGraphics("423 Park Avenue Huntington NY", "Huntington Youth Bureau");
            //Delay();
            AddGraphics("2760 Middle Country Road Lake Grove NY", "IMPACT Counseling Services");
            Delay();
            AddGraphics("30 Floyds Run Bohemia NY", "Institute for Rational Counseling");
            Delay();
            AddGraphics("208 Route 112 Port Jefferson NY", "John T Mather Memorial Hospital");
            Delay();
            AddGraphics("300 Motor Parkway Hauppauge NY", "The Kenneth Peters Center for Recovery");
            Delay();
            AddGraphics("320 West Montauk Hwy Hampton Bays NY", "Long Island Center for Recovery, Inc.");
            Delay();
            AddGraphics("877 East Main Street Riverhead NY", "Long Island Council on Alcoholism & Drug Dependence");
            Delay();
            AddGraphics("2865 Veteran’s Memorial Highway Ronkonkoma NY", "Long Island Council on Alcoholism & Drug Dependence");
            Delay();
            //AddGraphics("240 West Main Street Riverhead NY", "Maryhaven Center of Hope, Inc.");
            //Delay();
            AddGraphics("11 Farber Drive Bellport NY", "Outreach Development Corporation");
            Delay();
            AddGraphics("400 Crooked Hill Road Brentwood NY", "Outreach Development Corporation");
            Delay();
            AddGraphics("55 Horizon Drive Huntington NY", "The Pederson-Krag Center, Inc.");
            Delay();
            AddGraphics("11 Route 111 Smithtown NY", "The Pederson-Krag Center, Inc.");
            Delay();
            //AddGraphics("240 Long Island Avenue Wyandanch NY", "The Pederson-Krag Center, Inc.");
            //Delay();
            AddGraphics("998 Crooked Hill Road West Brentwood NY", "Phoenix Houses of L.I., Inc");
            Delay();
            //AddGraphics("283 Springs Fireplace Road,East Hampton NY", "Phoenix Houses of L.I., Inc");
            //Delay();
            //AddGraphics("95 Industrial Road Wainscott NY", "Phoenix Houses of L.I., Inc");
            //Delay();
            //AddGraphics("400 Sunrise Highway Amityville NY", "Prevention Resource Center");
            //Delay();
            //AddGraphics("542 E Main St Riverhead NY", "Riverhead Community Awareness Program, Inc (CAP)");
            AddGraphics("200 Belle Terre Road Port Jefferson NY", "St. Charles Hospital");
            Delay();
            AddGraphics("3251 Route 112 Medford NY", "Seafield Services, Inc.");
            Delay();
            AddGraphics("468 Boyle Road Port Jefferson Station NY", "Sunshine Prevention Center Youth and Family Services");
            Delay();
            AddGraphics("4949 Expressway Drive No Ronkonkoma NY", "YMCA of Long Island Inc, Family Service Program");
            //AddGraphics("", "");
            //AddGraphics("", "");










//,St. Charles Hospital,200 Belle Terre Road,Port Jefferson,,11777,,,,
//,"Sanctuary East, LTD",2 William Avenue,East Islip,,11730,,,,
//,"Seafield Services, Inc.",37 John Street,Amityville,,11701,,,,
//,"Seafield Services, Inc.",3251 Route 112,Medford,,11763,,,,
//,"Seafield Services, Inc.",7 Seafield Lane,Westhampton Beach,,11977,,,,
//,"Seafield Services, Inc.",450 Waverly Avenue,Patchogue,,11772,,,,
//,"Seafield Services, Inc.",212 West Main Street,Riverhead,,11901,,,,
//,"Seafield Services, Inc.",Yaphank Ave.,Yaphank,,11980,,,,
//,South Oaks Hospital,400 Sunrise Highway,Amityville,,11701,,,,
//,SCO Family of Services Madonna Heights Morning Star I & II,151 Burrs Lane,Dix Hills,,11746,,,,
//,Suffolk Co. Dept of Health Services,725 Veterans Memorial Hwy,Hauppauge,,11760,,,,
//,Suffolk Co. Dept of Health Services,300 Center Drive,Riverhead,,11901,,,,
//,Sunshine Prevention Center Youth and Family Services,468 Boyle Road,Port Jefferson Station,,11776,,,,
//,Town/Babylon Division of Drug/Alcohol Services,281 Phelps Lane,North Babylon,,11703,,,,
//,Town/Babylon Division of Drug/Alcohol Services,400 Broadway,Amityville,,11707,,,,
//,Town of Smithtown – Horizons Counseling & Education Center,161 East Main Street,Smithtown,,11787,,,,
//,"YMCA of Long Island Inc, Family Service Program",4949 Expressway Drive No,Ronkonkoma,,11779,,,,
//,"YMCA of Long Island Inc, Family Service Program",324 Main Street,Northport,,11768,,,,




        }

        private void Delay()
        {
            for (int i = 0; i < 10; i++)
            {
                intFoo = 1;
            }

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
            // check if there is a previous pinpoint

           

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
                textSym.Color = Colors.DarkRed;
                
                textSym.Font = new Esri.ArcGISRuntime.Symbology.SymbolFont("Arial", 12);
                textSym.BackgroundColor = Colors.White;
                
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

                if (graphicsLayer.Graphics.Contains(oldGraphic))
                {
                    graphicsLayer.Graphics.Remove(oldGraphic);
                }

                graphicsLayer.Graphics.Add(matchGraphic);

                oldGraphic = matchGraphic;

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

        
        private void MyMapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }

        private async void LoadBusMap_Click(object sender, RoutedEventArgs e)
        {
            var from = await this.FindAddress("32 arlington road, lake ronkonkoma, ny");
            var to = await this.FindAddress("50 gettysburg drive, port jefferson, ny");

            try
            {
                 if (from == null)
                 {
                      throw new Exception("Unable to find a match for '"  + "'.");
                 }

                 if (to == null)
                 {
                      throw new Exception("Unable to find a match for '"  + "'.");
                 }

                 // get the RouteResults graphics layer; add the from/to graphics
                 var routeGraphics = MyMap.Layers["RouteResults"] as GraphicsLayer;
                 if (routeGraphics == null)
                 {
                      throw new Exception("A graphics layer named 'RouteResults' was not found in the map.");
                 }

     // code here to show address locations on the map
                 var fromMapPoint = GeometryEngine.Project(from.Geometry, new SpatialReference(4326));
                 var toMapPoint = GeometryEngine.Project(to.Geometry, new SpatialReference(4326));

                 var fromSym = new SimpleMarkerSymbol { Style = SimpleMarkerStyle.Circle, Size = 16, Color = Colors.Green };
                 var toSym = new SimpleMarkerSymbol { Style = SimpleMarkerStyle.Circle, Size = 16, Color = Colors.Red };

                 var fromMapGraphic = new Graphic { Geometry = fromMapPoint, Symbol = fromSym };
                 var toMapGraphic = new Graphic { Geometry = toMapPoint, Symbol = toSym };

                 routeGraphics.Graphics.Add(fromMapGraphic);
                 routeGraphics.Graphics.Add(toMapGraphic);

            }
            catch (Exception exp)
            {
                //this.DirectionsTextBlock.Text = exp.Message;
            }

        }
        private async System.Threading.Tasks.Task<Graphic> FindAddress(string address)
        {
            var uri = new Uri("http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer");
            var locator = new OnlineLocatorTask(uri, string.Empty);

            var findParams = new OnlineLocatorFindParameters(address);
            findParams.OutSpatialReference = new SpatialReference(4326);

            Graphic matchGraphic = null;
            var results = await locator.FindAsync(findParams, new System.Threading.CancellationToken());
            if (results.Count > 0)
            {
                var firstMatch = results[0].Feature;
                var matchLocation = firstMatch.Geometry as MapPoint;
                matchGraphic = new Graphic();
                matchGraphic.Geometry = matchLocation;
                matchGraphic.Attributes.Add("Name", address);
            }

            return matchGraphic;
        }

    }
}
