//https://codepen.io/udaymanvar/pen/MWaePBY
function initInput() {
  let input = document.createElement("input");
  input.type = "file";
  input.click();
}

function importData() {
  let fileReader = new FileReader();
  fileReader.onload = function (event) {
    return fileReader.result;
  };

  input.onchange = function (event) {
    fileReader.readAsText(event.target.files[0]);
  };
}
