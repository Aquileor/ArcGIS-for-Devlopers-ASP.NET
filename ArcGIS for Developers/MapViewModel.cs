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

            FeatureLayer trailHeadsLayer = new FeatureLayer(new Uri("https://services3.arcgis.com/GVgbJbqm8hXASVYi/arcgis/rest/services/Trailheads/FeatureServer/0"));

            FeatureLayer trailHeadsLines = new FeatureLayer(new Uri("https://services3.arcgis.com/GVgbJbqm8hXASVYi/arcgis/rest/services/Trails/FeatureServer/0"));

            FeatureLayer trailHeadsPoly = new FeatureLayer(new Uri("https://services3.arcgis.com/GVgbJbqm8hXASVYi/arcgis/rest/services/Parks_and_Open_Space/FeatureServer/0"));

            await trailHeadsLayer.LoadAsync();
            await trailHeadsLines.LoadAsync();
            await trailHeadsPoly.LoadAsync();

            newMap.OperationalLayers.Add(trailHeadsPoly);
            newMap.OperationalLayers.Add(trailHeadsLines);
            newMap.OperationalLayers.Add(trailHeadsLayer);
   

            newMap.InitialViewpoint = new Viewpoint(trailHeadsLayer.FullExtent);

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
