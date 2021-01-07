
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

document.getElementById('inp').onchange = function (e) {
    var img = new Image();
    img.onload = draw;
    img.onerror = failed;
    debugger
    img.src = URL.createObjectURL(this.files[0]);
    //img.src ="../image/placeholder.png"
};
function draw() {
    var canvas = document.getElementById('canvas');
    canvas.width = this.width;
    canvas.height = this.height;
    var ctx = canvas.getContext('2d');
    ctx.drawImage(this, 0, 0);
}
function failed() {
    console.error("The provided file couldn't be loaded as an Image media");
}