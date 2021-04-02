'use strict';

var gMeme;
var gCtx;
var img;
var gImgObj = img;
var increament = 0;
var persistCanvas;

function createGmeme(imgId) {
    return {
        selectedImgId: imgId,
        txts: [createTxt('', 150, 70)/*, createTxt('', 150, 300)*/]
    };

}

function createTxt(line, x, y) {

    return {
  
        //object txt = {property:value}
        line: line,
        size: 40,
        align: 'left',
        color: '#000000', // in color picker, if choosing color from platte notice it stays "solid".
        fontFamily: 'Impact',
        isOutline: true,
        lineWidth: 2, // outline width
        strokeStyle: '#ffffff',
        isShadow: false,
        shadowColor: '#000000',
        shadowOffsetX: 1,
        shadowOffsetY: 1,
        shadowBlur: 0,
        x: x,
        y: y
    };
}

function initMemeEditor(imgId) {
    debugger
    imgId = "#canvas";
    toggleView();
    gMeme = createGmeme(imgId);
  //  initCanvas();
    initEditor()
    renderTxtsEditor();
    createContainer(0);

}


function initCanvas() {

    var canvas = document.querySelector('.memeCanvas');
    gCtx = canvas.getContext('2d');

    gImgObj = new Image();
    gImgObj.src = getImgSrc();

    gImgObj.onload = function () {
        canvas.width = gImgObj.width;
        canvas.height = gImgObj.height;
        gMeme.txts[1].y = gImgObj.height - 70;

        drawCanvas();
    };


}

function getImgSrc() {
    // imgIdx needed to find img src url in gImg[]
    var imgIdx = gImgs.findIndex(function (img) {
        return gMeme.selectedImgId === img.id;
    });

    return gImgs[imgIdx].url;
}



//function drawCanvas() {
//    gCtx.drawImage(gImgObj, 0, 0);

//    gMeme.txts.forEach(function (txt) {
//        drawTxt(txt);
//    });

//}

function drawTxt(txt) {
    debugger
 
    gCtx.font = txt.size + 'px' + ' ' + txt.fontFamily;
    gCtx.textAlign = txt.align;
    gCtx.fillStyle = txt.color;
 
    if (txt.isShadow) addTxtShadow(txt);
    if (txt.isOutline) addTxtOutline(txt);

    gCtx.fillText(txt.line, txt.x, txt.y);
}

function addTxtShadow(txt) {
    gCtx.shadowColor = txt.shadowColor;
    gCtx.shadowOffsetX = txt.shadowOffsetX;
    gCtx.shadowOffsetY = txt.shadowOffsetY;
    gCtx.shadowBlur = txt.shadowBlur;
}

function addTxtOutline(txt) {
    gCtx.strokeStyle = txt.strokeStyle;
    gCtx.lineWidth = txt.lineWidth;
    gCtx.strokeText(txt.line, txt.x, txt.y);
}

/**
 * editTxt() gets changes for txt and update gMeme model.
 * Update gMeme.txts[].txt = {property:value}
 * Redraws canvas.
 * Input types: text, number, checkbox, dropdown.
 * 
 *  txtIdx - the specific txt to change in []. it's line num -1 because idx starts with 0.
 *  property - (using data-* attributes) ex. line, size, align, color, isShadow.. 
 *  value - ex. 'text', 30, left, red, true..
 */
function editTxt(elinput, txtIdx) {
    var property = elinput.dataset.property;  // using data-* attributes
    var value;
   
    switch (elinput.type) {
        case 'select-one':
            value = elinput.options[elinput.selectedIndex].value;
            break;
        case 'checkbox':
            value = elinput.checked;
            break;
        default: // text, number
            value = elinput.value;
            break;
    }
    gMeme.txts[txtIdx][property] = value;
    drawCanvas();
    //drawText();
}


function renderTxtsEditor() {
    var strHtml = gMeme.txts.map(function (txt, idx) {
        debugger
        return `
        <div class="txt-editor">
                   
                    <p>
                    <button onclick="deleteTxt(${idx})"><i class="fas fa-eraser"></i></button>
                    <input type="text" data-property="line" placeholder="${txt.line}" oninput="editText(this,${idx})">
                    <i class="fas fa-text-height"></i> <input type="range" value="${txt.size}"  min="10" step="2" data-property="size" oninput="editText(this ,${idx})">
                    <input type="color" value="${txt.color}" data-property="color" oninput="editText(this,${idx})">
                    Family: 
                    <select data-property="fontFamily" oninput="editText(this,${idx})">
                    <option value="${txt.fontFamily}">${txt.fontFamily}</option>
                    <option value="Tahoma">Tahoma</option>
                    <option value="Geneva">Geneva</option>
                    <option value="Verdana">Verdana</option>
                    </select>
                    </p>
                    <p>
                    <input id="outline" type="checkbox" data-property="isOutline" checked onclick="editText(this,${idx})">
                    <label for="outline">Outline</label>
                    Width: <input type="number" value="${txt.lineWidth}"  min="0" step="1" data-property="lineWidth" oninput="editText(this ,${idx})">
                    <input type="color" value="${txt.strokeStyle}" data-property="strokeStyle" oninput="editText(this,${idx})">
                    </p>
                    <p>
                    <input type='hidden' id='hiddenx' value="${txt.x}" />
                    <input type='hidden' id='hiddeny' value="${txt.y}" />
                    <input id="shadow" type="checkbox" data-property="isShadow" onclick="editText(this,${idx})">
                    <label for="shadow">Shadow</label>
                    <input type="color" value="${txt.shadowColor}" data-property="shadowColor" oninput="editText(this,${idx})">
                    <i class="fas fa-arrows-alt-h"></i> <input type="number" value="${txt.shadowOffsetX}"  step="1" data-property="shadowOffsetX" oninput="editText(this ,${idx})">
                    <i class="fas fa-arrows-alt-v"></i><input type="number" value="${txt.shadowOffsetY}"  step="1" data-property="shadowOffsetY" oninput="editText(this ,${idx})">
                    Blur: <input type="number" value="${txt.shadowBlur}" data-property="shadowBlur" oninput="editText(this,${idx})">
                    </p>
               
                </div>

        `    ;
     

    })
        .join(' ');

    document.querySelector('.txts-list').innerHTML = strHtml;

}

function newTxtBtnClicked() {
    debugger
    //var x = parseInt($('#hiddenx').html());
    //var y = parseInt($('#hiddeny').html());
    //var y = txt.y;  var x = txt.x;
 
    gMeme.txts.push(createTxt('', 150 , 150 + increament ));
    increament += 50;
    drawCanvas();
    //drawText();
    renderTxtsEditor();
    createContainer(gMeme.txts.length-1);
   
}

function deleteTxt(txtIdx) {
    debugger
    gMeme.txts.splice(txtIdx, 1); //arr.splice(start, deleteCount)
    drawCanvas();
   // drawText();
    renderTxtsEditor();
}


/* REGISTER DOWNLOAD HANDLER */
function dlCanvas(eldllink) {
    var canvas = document.getElementById('canvas');

    var dt = canvas.toDataURL('image/png');
    /* Change MIME type to trick the browser to downlaod the file instead of displaying it */
    dt = dt.replace(/^data:image\/[^;]*/, 'data:application/octet-stream');

    /* In addition to <a>'s "download" attribute, you can define HTTP-style headers */
    dt = dt.replace(/^data:application\/octet-stream/, 'data:application/octet-stream;headers=Content-Disposition%3A%20attachment%3B%20filename=canvas.png');

    eldllink.href = dt;
    
}

function toggleView() {
    document.querySelector('.meme-container').classList.toggle('hidden');
    document.querySelector('.gallery').classList.toggle('hidden');
}

function drawText() {
    //gCtx.drawImage(persistCanvas, 0, 0);
    //var p = document.getElementById('canvas');
    //gCtx = p.getContext('2d');
    //gMeme.txts.forEach(function (txt) {
    //    drawTxt(txt);
    //});

}

function createContainer(id) {
    var maincontainerid = "draggable-container" + id;
    var containerid = 'movable-para' + id;
    var mainContainer = document.createElement('div');
    mainContainer.id = maincontainerid;
    mainContainer.classList.add("drag-box");
    mainContainer.classList.add("resizable");
 //   mainContainer.dataset.add('property', 'text-container');


    var para = document.createElement('p');
    //para.font = txt.size + 'px';
    //para.fontFamily = txt.fontFamily;
    //para.textAlign = txt.align;
    para.color = "white";
    //para.style.shadowColor = txt.shadowColor;
    //para.style.shadowOffsetX= txt.shadowOffsetX;
    //para.style.shadowOffsetY = txt.shadowOffsetY;
    //para.style.shadowBlur = txt.shadowBlur;
   // para.innerHTML = element.value;
    para.id = containerid;
    para.style.fontSize = '40px';
    para.style.overflow = "hidden";

    var divlu = document.createElement('div');
    var divld = document.createElement('div');
    var divru = document.createElement('div');
    var divrd = document.createElement('div');
    divlu.classList.add('resize');
    divlu.classList.add('NW');
    divlu.classList.add('top-left');
    divld.classList.add('resize');
    divld.classList.add('SW');
    divld.classList.add('bottom-left');
    divru.classList = ['resize NE top-right'];
    divrd.classList = ['resize SE bottom-right'];

    mainContainer.appendChild(divlu);
    mainContainer.appendChild(divld);
    mainContainer.appendChild(divru);
    mainContainer.appendChild(divrd);

    mainContainer.appendChild(para);
    document.getElementById('preview-container').appendChild(mainContainer);

    var dragele = document.getElementsByClassName("drag-box")
    for (var i = 0; i < dragele.length; i++) {
        dragElement(document.getElementById(dragele[i].id));
    }
    makeResizableDiv('.resizable')
}

function editText(element, id) {
    debugger
    var inpType = element.dataset.property;
    var containerid = 'movable-para' + id;

 
        if (inpType === "size") {
            $('#' + containerid).css('font-size', element.value + 'px');
        }
        else if (inpType === "line") {
            $('#' + containerid).html("");
            $('#' + containerid).html(element.value);
        }
        else if (inpType === "fontFamily") {
            $('#' + containerid).css('font-family', element.value );
        }
        else if (inpType === "color") {
            $('#' + containerid).css('color', element.value);
        }
        else if (inpType === "isShadow") {
            if ($('input[id="shadow"]:checked').length >0) {

            }
        
    }
  

    //$('#draggable-container').html(element.innerHTML);
}

function makeResizableDiv(div) {
    debugger
    const element = document.querySelector(div);
    const resizers = document.querySelectorAll(div + ' .resize')
    const minimum_size = 20;
    let original_width = 0;
    let original_height = 0;
    let original_x = 0;
    let original_y = 0;
    let original_mouse_x = 0;
    let original_mouse_y = 0;
    for (let i = 0; i < resizers.length; i++) {
        const currentResizer = resizers[i];
        currentResizer.addEventListener('mousedown', function (e) {
            e.preventDefault()
            original_width = parseFloat(getComputedStyle(element, null).getPropertyValue('width').replace('px', ''));
            original_height = parseFloat(getComputedStyle(element, null).getPropertyValue('height').replace('px', ''));
            original_x = element.getBoundingClientRect().left;
            original_y = element.getBoundingClientRect().top;
            original_mouse_x = e.pageX;
            original_mouse_y = e.pageY;
            window.addEventListener('mousemove', resize)
            window.addEventListener('mouseup', stopResize)
        })

        function resize(e) {
            debugger
            if (currentResizer.classList.contains('bottom-right')) {
                const width = original_width + (e.pageX - original_mouse_x);
                const height = original_height + (e.pageY - original_mouse_y)
                if (width > minimum_size) {
                    element.style.width = width + 'px'
                }
                if (height > minimum_size) {
                    element.style.height = height + 'px'
                }
            }
            else if (currentResizer.classList.contains('bottom-left')) {
                const height = original_height + (e.pageY - original_mouse_y)
                const width = original_width - (e.pageX - original_mouse_x)
                if (height > minimum_size) {
                    element.style.height = height + 'px'
                }
                if (width > minimum_size) {
                    element.style.width = width + 'px'
               //     element.style.left = original_x + (e.pageX - original_mouse_x) + 'px'
                }
            }
            else if (currentResizer.classList.contains('top-right')) {
                const width = original_width + (e.pageX - original_mouse_x)
                const height = original_height - (e.pageY - original_mouse_y)
                if (width > minimum_size) {
                    element.style.width = width + 'px'
                }
                if (height > minimum_size) {
                    element.style.height = height + 'px'
                   // element.style.top = original_y + (e.pageY - original_mouse_y) + 'px'
                }
            }
            else {
                const width = original_width - (e.pageX - original_mouse_x)
                const height = original_height - (e.pageY - original_mouse_y)
                if (width > minimum_size) {
                    element.style.width = width + 'px'
                 //   element.style.left = original_x + (e.pageX - original_mouse_x) + 'px'
                }
                if (height > minimum_size) {
                    element.style.height = height + 'px'
                   // element.style.top = original_y + (e.pageY - original_mouse_y) + 'px'
                }
            }
        }

        function stopResize() {
            window.removeEventListener('mousemove', resize)
        }
    }
}

makeResizableDiv('.resizable')