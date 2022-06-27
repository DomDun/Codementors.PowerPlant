using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.Linq;

namespace PowerPlantCzarnobyl.Wcf.SelfhostServer
{
    public class Mapper
    {
        public Member MapToDomainMember(MemberWcf memberWcf)
        {
            return new Member
            {
                Login = memberWcf.Login,
                Password = memberWcf.Password,
                Role = memberWcf.Role
            };
        }

        public MemberWcf MapToContractMember(Member member)
        {
            return new MemberWcf
            {
                Login = member.Login,
                Password = member.Password,
                Role = member.Role
            };
        }

        public PowerPlantDataSetWcf MapToContractDataFromPlant(PowerPlantDataSetData recievedData)
        {
            return new PowerPlantDataSetWcf
            {
                PlantName = recievedData.PlantName,
                Cauldrons = recievedData.Cauldrons
                .Select(c => new CauldronDataWcf
                {
                    Name = c.Name,
                    WaterPressure = new AssetParameterDataWcf()
                    {
                        MinValue = c.WaterPressure.MinValue,
                        MaxValue = c.WaterPressure.MaxValue,
                        TypicalValue = c.WaterPressure.TypicalValue,
                        CurrentValue = c.WaterPressure.CurrentValue,
                        Unit = c.WaterPressure.Unit
                    },

                    WaterTemperature = new AssetParameterDataWcf()
                    {
                        MinValue = c.WaterTemperature.MinValue,
                        MaxValue = c.WaterTemperature.MaxValue,
                        TypicalValue = c.WaterTemperature.TypicalValue,
                        CurrentValue = c.WaterTemperature.CurrentValue,
                        Unit = c.WaterTemperature.Unit
                    },
                    CamberTemperature = new AssetParameterDataWcf()
                    {
                        MinValue = c.CamberTemperature.MinValue,
                        MaxValue = c.CamberTemperature.MaxValue,
                        TypicalValue = c.CamberTemperature.TypicalValue,
                        CurrentValue = c.CamberTemperature.CurrentValue,
                        Unit = c.CamberTemperature.Unit
                    }
                })
                .ToArray(),
                Turbines = recievedData.Turbines
                 .Select(t => new TurbineDataWcf
                 {
                     Name = t.Name,
                     OverheaterSteamTemperature = new AssetParameterDataWcf
                     {
                         MinValue = t.OverheaterSteamTemperature.MinValue,
                         MaxValue = t.OverheaterSteamTemperature.MaxValue,
                         TypicalValue = t.OverheaterSteamTemperature.TypicalValue,
                         CurrentValue = t.OverheaterSteamTemperature.CurrentValue,
                         Unit = t.OverheaterSteamTemperature.Unit
                     },
                     SteamPressure = new AssetParameterDataWcf
                     {
                         MinValue = t.SteamPressure.MinValue,
                         MaxValue = t.SteamPressure.MaxValue,
                         TypicalValue = t.SteamPressure.TypicalValue,
                         CurrentValue = t.SteamPressure.CurrentValue,
                         Unit = t.SteamPressure.Unit
                     },
                     RotationSpeed = new AssetParameterDataWcf
                     {
                         MinValue = t.RotationSpeed.MinValue,
                         MaxValue = t.RotationSpeed.MaxValue,
                         TypicalValue = t.RotationSpeed.TypicalValue,
                         CurrentValue = t.RotationSpeed.CurrentValue,
                         Unit = t.RotationSpeed.Unit
                     },
                     CurrentPower = new AssetParameterDataWcf
                     {
                         MinValue = t.CurrentPower.MinValue,
                         MaxValue = t.CurrentPower.MaxValue,
                         TypicalValue = t.CurrentPower.TypicalValue,
                         CurrentValue = t.CurrentPower.CurrentValue,
                         Unit = t.CurrentPower.Unit
                     },
                     OutputVoltage = new AssetParameterDataWcf
                     {
                         MinValue = t.OutputVoltage.MinValue,
                         MaxValue = t.OutputVoltage.MaxValue,
                         TypicalValue = t.OutputVoltage.TypicalValue,
                         CurrentValue = t.OutputVoltage.CurrentValue,
                         Unit = t.OutputVoltage.Unit
                     },
                 })
                .ToArray(),
                Transformators = recievedData.Transformators
                .Select(t => new TransformatorDataWcf
                {
                    Name = t.Name,
                    InputVoltage = new AssetParameterDataWcf
                    {
                        MinValue = t.InputVoltage.MinValue,
                        MaxValue = t.InputVoltage.MaxValue,
                        TypicalValue = t.InputVoltage.TypicalValue,
                        CurrentValue = t.InputVoltage.CurrentValue,
                        Unit = t.InputVoltage.Unit
                    },
                    OutputVoltage = new AssetParameterDataWcf
                    {
                        MinValue = t.OutputVoltage.MinValue,
                        MaxValue = t.OutputVoltage.MaxValue,
                        TypicalValue = t.OutputVoltage.TypicalValue,
                        CurrentValue = t.OutputVoltage.CurrentValue,
                        Unit = t.OutputVoltage.Unit
                    },
                })
                .ToArray()
            };
        }

        internal Error MapToDomainError(ErrorWcf error)
        {
            return new Error
            {
                PlantName = error.PlantName,
                MachineName = error.MachineName,
                Parameter = error.Parameter,
                ErrorTime = error.ErrorTime,
                LoggedUser = error.LoggedUser,
                MinValue = error.MinValue,
                MaxValue = error.MaxValue,
            };
        }

        internal InspectionWcf MapToContractInspection(Inspection inspection)
        {
            return new InspectionWcf
            {
                Id = inspection.Id,
                MachineName = inspection.MachineName,
                CreateDate = inspection.CreateDate,
                UpdateDate = inspection.UpdateDate,
                EndDate = inspection.EndDate,
                Comments = inspection.Comments,
                State = (ServiceDefinitions.Models.State)inspection.State,
                Engineer = inspection.Engineer
            };
        }

        public Inspection MapToDomainInspection(InspectionWcf inspectionWcf)
        {
            return new Inspection
            {
                Id =  inspectionWcf.Id,
                MachineName = inspectionWcf.MachineName,
                CreateDate = inspectionWcf.CreateDate,
                UpdateDate = inspectionWcf.UpdateDate,
                EndDate = inspectionWcf.EndDate,
                Comments = inspectionWcf.Comments,
                State = (Domain.Models.State)inspectionWcf.State,
                Engineer = inspectionWcf.Engineer
            };
        }

        public ErrorWcf MapToContractError(Error error)
        {
            return new ErrorWcf
            {
                PlantName = error.PlantName,
                MachineName = error.MachineName,
                Parameter = error.Parameter,
                ErrorTime = error.ErrorTime,
                LoggedUser = error.LoggedUser,
                MinValue = error.MinValue,
                MaxValue = error.MaxValue,
            };
        }
    }
}
