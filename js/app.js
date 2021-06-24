// import Something from "./another-js-file.js";

class App {
    constructor() {
    }
    go() {
        loadBooks();
    }
}
new App().go();

function loadBooks() {
    fetch('/api/books')
        .then(response => response.json())
        .then(function (data) {
            for (var i = 0; i < data.length; i++) {
                addRow('books', data[i].Title, data[i].Id);
            }
        })
        .catch(function (response) {
            console.log(response)
        })
}
function addRow(tableID, title, id) {
    let tableRef = document.getElementById(tableID);
    let newRow = tableRef.insertRow(-1);
    newRow.setAttribute("onclick", "javascript:getData(" + id + ",\"" + title + "\"); return false");
    let newCell = newRow.insertCell(0);
    newCell.innerHTML = title;
}
function getData(id, title) {
    document.getElementById('lblMessage').innerHTML = "";
    document.getElementById('lblId').innerHTML = "";
    document.getElementById('txtSearch').value = "";
    document.getElementById('lblTitle').innerHTML = title;
    fetch('/api/books/' + id)
        .then(response => response.json())
        .then(function (data) {
            let tableRef = document.getElementById('tblWords');
            while (tableRef.hasChildNodes()) {
                tableRef.removeChild(tableRef.lastChild);
            }
            document.getElementById('lblMessage').innerHTML = "Most common words in <br />\"" + title + "\"";
            document.getElementById('lblId').innerHTML = id;

            for (var i = 0; i < data.length; i++) {
                let newRow = tableRef.insertRow(-1);
                let newCell = newRow.insertCell(0);
                newCell.innerHTML = data[i].Word;
                let newCell1 = newRow.insertCell(1);
                newCell1.innerHTML = data[i].Count;
            }
        })
        .catch(function (response) {
            console.log(response)
        })
}
function searchWord() {
    var key = document.getElementById('txtSearch').value;
    var Id = document.getElementById('lblId').innerHTML;
    var title = document.getElementById('lblTitle').innerHTML;
    if (key.length < 3) {
        getData(Id, title);
        return;
    }
    document.getElementById('lblMessage').innerHTML = "";
    fetch('/api/books/' + Id + "?query=" + key)
        .then(response => response.json())
        .then(function (data) {
            let tableRef = document.getElementById('tblWords');
            while (tableRef.hasChildNodes()) {
                tableRef.removeChild(tableRef.lastChild);
            }
            document.getElementById('lblMessage').innerHTML = "Words in '" + title + "' starting with '" + key + "'";
            for (var i = 0; i < data.length; i++) {
                let newRow = tableRef.insertRow(-1);
                let newCell = newRow.insertCell(0);
                newCell.innerHTML = data[i].Word;
                let newCell1 = newRow.insertCell(1);
                newCell1.innerHTML = data[i].Count;
            }
        })
        .catch(function (response) {
            console.log(response)
        })
}

function ChangeValue(e) {
    if (e.value.length >= 3)
        searchWord();
    else {

        var Id = document.getElementById('lblId').innerHTML;
        var title = document.getElementById('lblTitle').innerHTML;
        var txtVal = e.value;
        getData(Id, title);
        document.getElementById('txtSearch').value = txtVal;
    }
}