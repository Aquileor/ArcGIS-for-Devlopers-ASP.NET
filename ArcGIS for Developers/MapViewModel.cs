using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;

namespace ArcGIS_for_Developers
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            CreateNewMap();
        }

        private async void CreateNewMap()
        {
            Map newMap = new Map(Basemap.CreateNationalGeographic());

            FeatureLayer NeaplAdmin1 = new FeatureLayer(new Uri("https://services.arcgis.com/iQ1dY19aHwbSDYIF/arcgis/rest/services/GAFSP_Nepal_2016/FeatureServer/1"));

            FeatureLayer NeaplandFood = new FeatureLayer(new Uri("https://services.arcgis.com/iQ1dY19aHwbSDYIF/arcgis/rest/services/GAFSP_Nepal_2016/FeatureServer/2"));

            FeatureLayer NeaplAirport = new FeatureLayer(new Uri("https://services.arcgis.com/pGfbNJoYypmNq86F/arcgis/rest/services/Nepal_Places_Facilities/FeatureServer/2"));

            await NeaplAirport.LoadAsync();
            await NeaplAdmin1.LoadAsync();
            await NeaplandFood.LoadAsync();

            newMap.OperationalLayers.Add(NeaplandFood);
            newMap.OperationalLayers.Add(NeaplAdmin1);
            newMap.OperationalLayers.Add(NeaplAirport);
   

            newMap.InitialViewpoint = new Viewpoint(NeaplAdmin1.FullExtent);

            Map = newMap;
        }

        private Map _map = new Map(Basemap.CreateStreets());

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
