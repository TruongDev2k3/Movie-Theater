
var btnPrev = document.querySelector('.prev');
var btnNext = document.querySelector('.next');
// console.log(btnNext)

var count = 1;
btnNext.addEventListener('click', e=>{
    
    document.getElementById('radio' + count).checked = true;
    
    if(count == 5){
        setTimeout("count = 1",1000)
        
    }else {
        count++;
    }
})
btnPrev.addEventListener('click', e=>{
    document.getElementById('radio' + count).checked = true;
    //  NẾU VỊ TRÍ INDEX CỦA ẢNH = 1 - TỨC LÀ ẢNH ĐẦU TIÊN => KHI CLICK QUAI LẠI THÌ ẢNH SẼ NHẬN GIÁ TRỊ INDEX CUỐI DANH SÁCH
    if(count == 1){
        count = 5
    }
    // CÒN KHÔNG THÌ GIẢM GIÁ TRỊ VỊ TRÍ INDEX
    else{
        count--
    }
    
})

var count = 1;
setInterval(function(){
    document.getElementById('radio' + count).checked = true;
    count++;
    if(count > 5){
        count = 1
    }

},6000)



var btn_Prev = document.querySelector('.btn-prev');
var btn_Next = document.querySelector('.btn-next');
var cow = document.querySelector('.animation-list');

btn_Next.addEventListener('click', e=>{
    btn_Next.style.display = 'none';
    cow.style.marginLeft = "-40%"
    btn_Prev.style.display = 'inline-block';     
})
btn_Prev.addEventListener('click', e=>{
    btn_Next.style.display = 'inline-block';
    cow.style.marginLeft = "0"
    btn_Prev.style.display = 'none';     
})

// JAVASCRIPT MỤC FLASHSALE
var btn_Prev1 = document.querySelector('.btn-prev-1');
var btn_Next1 = document.querySelector('.btn-next-1');
var anmSale = document.querySelector('.animation-product-sale');

btn_Next1.addEventListener('click', e=>{  
    btn_Next1.style.display = 'none'
    anmSale.style.marginLeft = "-85%"    
    btn_Prev1.style.display = 'block'
})
btn_Prev1.addEventListener('click', e=>{
    anmSale.style.marginLeft = "0"; 
    btn_Next1.style.display = 'inline-block' 
    btn_Prev1.style.display = 'none'   
})

// JS SLIDE 2
var btnPr = document.querySelector('.Prev');
var btnNxt = document.querySelector('.Next');
var counts = 1;
btnNxt.addEventListener('click', e=>{
    
    document.getElementById('Radio' + counts).checked = true;
    
    if(count == 5){
        setTimeout("counts = 1",1000)
        
    }else {
        counts++;
    }
})
btnPr.addEventListener('click', e=>{
    document.getElementById('Radio' + counts).checked = true;
    //  NẾU VỊ TRÍ INDEX CỦA ẢNH = 1 - TỨC LÀ ẢNH ĐẦU TIÊN => KHI CLICK QUAI LẠI THÌ ẢNH SẼ NHẬN GIÁ TRỊ INDEX CUỐI DANH SÁCH
    if(counts == 1){
        counts = 5
    }
    // CÒN KHÔNG THÌ GIẢM GIÁ TRỊ VỊ TRÍ INDEX
    else{
        counts--
    }  
})
setInterval(function(){
    document.getElementById('Radio' + counts).checked = true;
    counts++;
    if(counts > 5){
        counts = 1
    }

},3000)


// js nuts aans sang shopee mall
var btn_PrevMall = document.querySelector('.btn-prevMall');
var btn_NextMall = document.querySelector('.btn-nextMall');
var anm_spMall = document.querySelector('.group-animation');

btn_NextMall.addEventListener('click', e=>{
    btn_NextMall.style.display = 'none';
    anm_spMall.style.marginLeft = "-75%"
    btn_PrevMall.style.display = 'inline-block';     
})
btn_PrevMall.addEventListener('click', e=>{
    btn_NextMall.style.display = 'inline-block';
    anm_spMall.style.marginLeft = "0"
    btn_PrevMall.style.display = 'none';     
})

// JS NÚT TRÁI PHẢI KHỐI TOP SEARCH
var next_top = document.querySelector('.next-top');
var prev_top = document.querySelector('.prev-top');
var anm_spTop = document.querySelector('.animation-top-search');

next_top.addEventListener('click', e=>{
    next_top.style.display = 'none';
    anm_spTop.style.marginLeft = "-85%"
    prev_top.style.display = 'block';     
})
prev_top.addEventListener('click', e=>{
    next_top.style.display = 'inline-block';
    anm_spTop.style.marginLeft = "0"
    prev_top.style.display = 'none';     
})