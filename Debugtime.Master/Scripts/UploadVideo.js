 

//$('#btnUpload').click(function () {
//    SaveFile($('#fileItem').val());

//});

function SaveFile(fileName)
{
    debugger;
    $.ajax({
        url: 'http://localhost:7727/api/course/AddVideoName',
        type: 'POST',
        data: { data: str },
        dataType:'json',
        error: function (data) {
            debugger;
        },
        success: function (data, textStatus, jqXHR) {
            debugger;
            //window.location.href = "url";
        }
    });
}


function UploadFile(TargetFile) {
    var uploadFileName = $('#fileItem').val();
    $('#uploadFileName').val(uploadFileName);
    // create array to store the buffer chunks
    var FileChunk = [];
    // the file object itself that we will work with
    var file = TargetFile[0];
   
    // set up other initial vars
    var MaxFileSizeMB = 1;
    var BufferChunkSize = MaxFileSizeMB * (1024 * 1024);
    var ReadBuffer_Size =1024;
    var FileStreamPos = 0;
    // set the initial chunk length
    var EndPos = BufferChunkSize;
    var Size = file.size;

    console.log('start while loop 1');
    // add to the FileChunk array until we get to the end of the file
    while (FileStreamPos < Size) {
        // "slice" the file from the starting position/offset, to  the required length
        FileChunk.push(file.slice(FileStreamPos, EndPos));
        FileStreamPos = EndPos; // jump by the amount read
        EndPos = FileStreamPos + BufferChunkSize; // set next chunk length
    }
    console.log('end while loop 1');
    // get total number of "files" we will be sending
    var TotalParts = FileChunk.length;
    var PartCount = 0;
    // loop through, pulling the first item from the array each time and sending it
    console.log('start while loop 2');
    while (chunk = FileChunk.shift()) {
        PartCount++;
        // file name convention
        var FilePartName = file.name + ".part_" + PartCount + "." + TotalParts;
        // send the file
        console.log('calliing uploadFileChunk');
        UploadFileChunk(chunk, FilePartName);
    }
    console.log('end while loop 2');
}


function UploadFileChunk(Chunk, FileName) {
    debugger;
    var FD = new FormData();
    FD.append('file', Chunk, FileName);
    $.ajax({
        url: 'http://localhost:7727/api/course/AddVideoInfo',
        type: 'POST',
        data: FD,
        //dataType:'json',
        processData: false,
        contentType: false,

        error: function (data, textStatus, errorThrown) {
            console.log('Erros', +textStatus);
            debugger;
        },
        success: function (data, textStatus, jqXHR) {
            debugger;
            //window.location.href = "url";
        }
    });
}