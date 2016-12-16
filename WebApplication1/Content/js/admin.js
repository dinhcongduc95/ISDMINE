var hostname = "";
var controller = "";
var form = null;
var action = "";
var idChecked = 0;
$(document).ready(function() {

    hostname = 'http://' + window.location.hostname + '/' + controller;
    form = document.getElementById('form');
    action = $('#action').val();


    function submitButton(action) {
        hostname = 'http://' + window.location.hostname + '/' + controller;
        form = document.getElementById('form');
        switch(action) {
        
            case 'add':
                doAdd();
                break;
            case 'edit':
                doEdit();
                break;
            case 'delete':
                doDelete();
                break;
        }
    }

    function doAddNew() {
        window.location = hostname + '/Create';
        return;
    }

    function doEdit() {
        window.location = hostname + '/Edit/' + idChecked;
    }
})