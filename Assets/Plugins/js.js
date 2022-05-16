//https://codepen.io/udaymanvar/pen/MWaePBY

function importData() {
    let input = document.createElement('input');
    input.type = 'file';
    input.onchange = _ => {
        // you can use this method to get file and perform respective operations
        let files = Array.from(input.files);
        var reader = new FileReader();
        reader.onload = function () {
            console.log(reader.result);
        };
        reader.readAsText(files[0]);
        console.log(files);
    };
    input.click();

}