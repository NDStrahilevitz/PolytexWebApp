function createValidLists(){
    console.log("HELLO");
    function isDispenser(model){
        console.log(model);
        return model === "D200" || model === "D300"; 
    }

    function isRetriever(model){
        return model === "R100X" || model === "R110" || model === "R310X" || model === "R310"; 
    }

    function getDropList(id){
        var str = "zdevice_" + id;
        return document.getElementById(str);
    }

    function addOptionsToSelect(options, select){
        for(var i = 0; i < options.length; ++i){
            console.log("adding");
            var option = document.createElement('option');
            option.text = options[i];
            option.value = options[i];
            select.add(option);
        }
    }
    function removeOptions(selectbox) {
        var i;
        for(i = selectbox.options.length - 1 ; i >= 0 ; i--){
            selectbox.remove(i);
        }
    }

    var optionDict = {
        "compressorOptions": ["None", "115V", "220V"],
        "idDeviceDisOptions": ["HF", "LF", "UHF"],
        "idDeviceRetOptions": ["None", "Camera", "HF", "LF", "UHF"],
        "d200CellsOptions": ['10','12','15','18','20','24'],
        "d300CellsOptions": ['20','24','30','36','40','48']
    }


    var modelValue = document.getElementById("zdevice_deviceModel").value;
    var dropLists = [getDropList("compressor"), getDropList("idDevice"), getDropList("cells")];

    //remove previous options
    for(var i = 0; i < dropLists.length; i++){
        console.log("removing");
        removeOptions(dropLists[i]);
    }

    if(isDispenser(modelValue)){
        console.log("dispenser");
        addOptionsToSelect(optionDict["compressorOptions"], dropLists[0]);
        addOptionsToSelect(optionDict["idDeviceDisOptions"], dropLists[1]);
        if(modelValue === "D200")
            addOptionsToSelect(optionDict["d200CellsOptions"], dropLists[2]);
        if(modelValue === "D300")
            addOptionsToSelect(optionDict["d300CellsOptions"], dropLists[2]);
    }
    if(isRetriever(modelValue)){
        console.log("retriever");
        addOptionsToSelect(optionDict["idDeviceRetOptions"], dropLists[1]);
    }
}

function warningDateMessage(){
    console.log("warningDateMessage");
    var deliveryDate = new Date(document.getElementById("zorder_deliveryDate").value);
    var orderDate = new Date(document.getElementById("orderDate").value);
    var warningLabel = document.getElementById("delivery-rate-warning");
    var diff = (deliveryDate - orderDate)/(24*3600*1000);
    if(diff < 90){
        console.log("entered");
        warningLabel.innerText = "\nStandard delivery date is 90 days.\n Special delivery dates confirmation is subject to Polytex inventory status";
    }
    else{
        warningLabel.innerText = "";
    }
}
