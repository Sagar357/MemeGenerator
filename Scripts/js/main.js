
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
    var canvas = document.getElementById('canvas');
    //canvas.width = this.width;
    //canvas.height = this.height;
    var ctx = canvas.getContext('2d');
    ctx.drawImage(this, 30, 30 ,30 ,30);
}
function failed() {
    console.error("The provided file couldn't be loaded as an Image media");
}

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