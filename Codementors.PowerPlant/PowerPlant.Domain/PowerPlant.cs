using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;

namespace PowerPlantCzarnobyl.Domain
{
    public class PowerPlant
    {
        public string Name { get; }

        private Cauldron[] _cauldrons { get; set; }
        private Turbine[] _turbines { get; set; }
        private Transformator[] _transformators { get; set; }

        private Timer _generateDataSetTimer;

        public event EventHandler<PowerPlantDataSet> OnNewDataSetArrival = null;

        public static PowerPlant Instance = new PowerPlant("PowerPlant 1: Czarnobyl");

        internal PowerPlant(string name)
        {
            Name = name;

            CreateCauldrons();
            CreateTurbines();
            CreateTransformators();

            _generateDataSetTimer = new Timer
            {
                AutoReset = true,
                Interval = 500,
                Enabled = true
            };

            _generateDataSetTimer.Elapsed += GenerateDataSet;

            _generateDataSetTimer.Start();
        }

        private void GenerateDataSet(object sender, ElapsedEventArgs e)
        {
            var dataSet = new PowerPlantDataSet
            {
                PlantName = Name,
                Cauldrons = DeepClone(_cauldrons),
                Transformators = DeepClone(_transformators),
                Turbines = DeepClone(_turbines)
            };

            OnNewDataSetArrival?.Invoke(this, dataSet);
        }

        private T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        private void CreateCauldrons()
        {
            _cauldrons = new Cauldron[2];
            for (var i = 0; i < _cauldrons.Length; i++)
            {
                _cauldrons[i] = new Cauldron("Cauldron_" + i);
            }
        }

        private void CreateTurbines()
        {
            _turbines = new Turbine[2];
            for (var i = 0; i < _turbines.Length; i++)
            {
                _turbines[i] = new Turbine("Turbine_" + i);
            }
        }

        private void CreateTransformators()
        {
            _transformators = new Transformator[2];
            for (var i = 0; i < _transformators.Length; i++)
            {
                _transformators[i] = new Transformator("Transformator_" + i);
            }
        }
    }
}
