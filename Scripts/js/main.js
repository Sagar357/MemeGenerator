
$(document).ready(function(){
   $(".middle_content").click(function(){
    $(".sidenav").css("width","0px");
   });

   $(".main-bar").click(function(){
    $("#mySidenav").css("width","250px");
   });
   $(".closebtn").click(function(){
     $("#mySidenav").css("width","0px");
   });


     // add & Remove class
  $(".li_cmn").click(function () {
    if(!$(this).hasClass('List_active'))
    {    
        $(".li_cmn.List_active").removeClass("List_active");
        $(this).addClass("List_active");        
    }
  });
  
  //---end--here---//
});

    //-------open_list------//

    function openSubList1(ListName) {
      var i;
      var x = document.getElementsByClassName("main-grid");
      for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";  
      }
      document.getElementById(ListName).style.display = "block";  
    }
  
    //----end---here----//
 
function upload(e) {
    debugger
    var img = new Image();
    img.onload = drawImg;
    img.onerror = failed;
    debugger
    img.src = URL.createObjectURL(e.currentTarget.files[0]);
   //img.src ="../image/placeholder.png"
}
function drawImg() {
    //var canvas = document.getElementById('canvas');
    var canvas = document.getElementById('dragCanvas');
    canvas.width = this.width;
    canvas.height = this.height;
    var ctx = canvas.getContext('2d');
    ctx.drawImage(this, 0,0 );
}
function failed() {
    console.error("The provided file couldn't be loaded as an Image media");
}
// inspect block code start//

 //$(document).keydown(function(event){
 // if(event.keyCode==123){
 //      return false;
 //  }
 //  else if (event.ctrlKey && event.shiftKey && event.keyCode==73){        
 //           return false;
 //  }
 //});

 //$(document).on("contextmenu",function(e){        
 // e.preventDefault();
 //});
// end inspect block code//

//start control u source code block//
document.onkeydown = function (e) {
    if (e.ctrlKey &&
        (e.keyCode === 85)) {
        return false;
    }
};

//end control u source code block//



//function convertImgToBase64URL(url, callback, outputFormat) {
//    var img = new Image();
//    img.crossOrigin = 'Anonymous';
//    img.onload = function () {
//        var canvas = document.createElement('CANVAS'),
//            ctx = canvas.getContext('2d'), dataURL;
//        canvas.height = img.height;
//        canvas.width = img.width;
//        ctx.drawImage(img, 0, 0);
//        dataURL = canvas.toDataURL(outputFormat);
//        callback(dataURL);
//        canvas = null;
//    };
//    img.src = url;
//}
//convertImgToBase64URL('https://cdn.evilmartians.com/front/posts/optimizing-react-virtual-dom-explained/cover-a1d5b40.png', function (base64Img) {
//    alert('it works');
//    $('.output').find('img').attr('src', base64Img);

//});



function dragElement(elmnt) {
    debugger
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    if (document.getElementById(elmnt.id + "header")) {
        // if present, the header is where you move the DIV from:
        document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
    } else {
        // otherwise, move the DIV from anywhere inside the DIV:
        elmnt.onmousedown = dragMouseDown;
    }

    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }

    function closeDragElement() {
        // stop moving when mouse button is released:
        document.onmouseup = null;
        document.onmousemove = null;
    }
}

var dragele = document.getElementsByClassName("drag-box")
for (var i = 0; i < dragele.length; i++) {
    dragElement(document.getElementById(dragele[i].id));
}
//dragele.forEach(function (item, index) {
//    debugger
//    dragElement(document.getElementsById(item.id));
//});

//function mouseMove(e) {
//    debugger
  
//    var box = $(".drag-box");
//    var boxCenter = [box.offset().left + box.width() / 2, box.offset().top + box.height() / 2];

//    var angle = Math.atan2(e.pageX - boxCenter[0], - (e.pageY - boxCenter[1])) * (180 / Math.PI);      

//}