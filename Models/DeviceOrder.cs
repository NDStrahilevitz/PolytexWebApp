using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolytexWebApp.Models
{
    public class DeviceOrder : IValidatableObject
    {
        //primary key
        public uint deviceNumber{get;set;}

        [Required]
        public string deviceModel{get;set;}

        //foreign key from purchase_orders
        [Required]
        public ulong poNumber{get;set;}

        [Required]
        public string voltage{get;set;}
        public string compressor{get;set;}
        [Required]
        public string idDevice{get;set;}
        [Required]
        public string cardReader{get;set;}
        public int cells{get;set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext context){
            var dispenserList = new List<string>()
                {"D200", "D300"};
            var retriverList = new List<string>()
                {"R100X", "R110", "R310X", "R310"};
            var modelList = new List<string>();
            modelList.AddRange(dispenserList);
            modelList.AddRange(retriverList);
            var voltageList = new List<string>()
                {"115V", "220V"};
            var compressorList = new List<string>()
                {"None", "115V", "220V"};
            var rfidList = new List<string>()
                {"HF", "LF", "UHF"};
            var idDevList = new List<string>()
                {"None", "Camera" };
            idDevList.AddRange(rfidList);
            var cardRdList = new List<string>()
                {"None", "Swipe Card", "Mifare", "125khz"};
            var cellsListD200 = new List<int>()
                {10,12,15,18,20,24};
            var cellsListD300 = new List<int>()
                {20,24,30,36,40,48};

            bool validModel = modelList.Contains(deviceModel);
            bool isDispenser = dispenserList.Contains(deviceModel);
            bool isRetriever = retriverList.Contains(deviceModel);
            bool validVoltage = voltageList.Contains(voltage);
            bool validCompressor = isRetriever && compressor == null || compressorList.Contains(compressor) && isDispenser;
            bool validIdDev = rfidList.Contains(idDevice) && isDispenser || idDevList.Contains(idDevice) && isRetriever;
            bool validCardRd = cardRdList.Contains(cardReader);
            bool validCells = (cells == 0 && isRetriever) || cellsListD200.Contains(cells) && deviceModel == "D200" || 
                                                                                        cellsListD300.Contains(cells) && deviceModel == "D300";
            bool allValid = validModel && validVoltage && validCompressor && validIdDev && validCardRd && validCells;
            if(!validModel)
                yield return new ValidationResult("Invalid Model");
            if(!validVoltage)
                yield return new ValidationResult("Invalid Voltage");
            if(!validCompressor)
                yield return new ValidationResult("Invalid Compressor or Invalid model");
            if(!validIdDev)
                yield return new ValidationResult("Invalid RFID/Camera Device or invalid model for selected device");
            if(!validCardRd)
                yield return new ValidationResult("Invalid Card Reader");
            if(!validCells){
                yield return new ValidationResult("Invalid Cell selection or invalid model for cells");
            }

            if(allValid){
                yield return ValidationResult.Success;
            }
        }
    }
}