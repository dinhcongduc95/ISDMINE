$('#btnChooseImage')
                    .click(function (e) {
                        e.preventDefault();
                        browseServer();
                    });

function browseServer() {
    $("#imagePath").focus();
    var finder = new CKFinder();
    finder.selectActionFunction = selectImageFunction;
    finder.language = 'vi';
    finder.popup();
}

function selectImageFunction(fileUrl) {
    $('#ImageLink').val(unescape(fileUrl));
    $('#Thumbnail').attr('src', fileUrl);
}