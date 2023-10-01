// lấy ra nút thêm vào giỏ hàng
var btn_addCart = document.querySelectorAll('.btn-add-cart')


// dùng foreach lấy ra the button va index của nó
btn_addCart.forEach(function(button, index) {
    button.addEventListener('click', function(event) {
        // khai bao btn item khi click vào chọn đúng phần tử đã click
        var btnItem = event.target;
        // lấy ra thẻ cha chứa khối vừa click
        var product = button.parentElement;
        // console.log(product)
        // lấy ảnh sản phẩm
        var productImg = product.querySelector('img').src
        // lấy tên sản phẩm
        var productName = product.querySelector('.title-img-product').innerText
        // Lấy ra giá sản phẩm
        var productPrice = product.querySelector('.price-img-product').innerText
        addCart(productPrice, productImg, productName)
    })
})
function addCart(productPrice, productImg, productName) {
    // tạo thẻ tr bên html
    var addTr = document.createElement('tr')
    // lấy ra thẻ tất cả thẻ Tr để đếm số lượng không cho phép chọn trùng
    var cartItem = document.querySelectorAll('tbody tr')
    for (var i = 0 ; i<cartItem.length; i++) {
        // lặp hết để lấy ra tên xem trung không
        var productN = document.querySelectorAll('.title')
        if (productN[i].innerHTML == productName){
            alert('Sản Phẩm Đã Có Trong Giỏ Hàng')
            return
        }
    }
    var TrContent = '<tr><td style="display: flex; align-items: center;"><img style="width: 70px; margin-right: 10px; " src="'+productImg+'" alt=""><span class="title">'+productName+'</span></td><td> <p><span class="price">'+productPrice+'</span><sup>đ</sup></p></td><td><input style="width: 30px; outline: none; text-align: center;" type="number" value="1" min="0"></td><td><button class="delete-cart" style="cursor: pointer;">Xóa</button></td></tr>'
    //  lấy ra khung chứa thẻ tr
    // hien thi tr
    addTr.innerHTML = TrContent
    // tr content
    
    var cartTable = document.querySelector('tbody')
    // console.log(cartTable)
    // add thẻ tr vào tbody và thêm vào cuối cùng
    cartTable.append(addTr)
    cartTotal()
    deleteCart()
}


// js tính tổng tiền khi lặp sẽ reset số lượng tất các sản phẩm
function cartTotal() {
    // lấy ra thẻ tất cả thẻ Tr để đếm số lượng
    var cartItem = document.querySelectorAll('tbody tr')
    // console.log(cartItem)
    var Total = 0;
    for ( var i=0; i<cartItem.length; i++){
        // Lấy ra giá trị số lượng từ thẻ input
        var inputValue = cartItem[i].querySelector('input').value
        // console.log(inputValue)
        // lấy ra giá sản phẩm
        var productPrice = cartItem[i].querySelector('.price').innerText
        // console.log(productPrice)
        // su dung ham tolocallestring để tạo dấu chấm ở giá
        var TotalPrice = inputValue * productPrice * 1000
        // var Totals = Total.toLocaleString('de-DE') 
        // console.log(TotalPrice)
        Total = Total + TotalPrice;
        // console.log(Total)
        
    }
    var cartTotalPrice = document.querySelector('.price-total span')
    cartTotalPrice.innerHTML = Total.toLocaleString('de-DE') 

    changeInput();
    changeTotalCart();
}

// js nút xóa sản phẩm trong giỏ hàng
function deleteCart() {
    var cartItem = document.querySelectorAll('tbody tr')
    for (var i = 0 ; i<cartItem.length; i++) {
        
        var btnDelete = document.querySelectorAll('.delete-cart')
        btnDelete[i].addEventListener("click", function(event){
            var cartDelete = event.target
            var CartItem = cartDelete.parentElement.parentElement
            CartItem.remove();
            cartTotal();
        })
        
       
    }
}


// js khi thay đổi số lượng tiền thay đổi theo
function changeInput(){
    var cartItem = document.querySelectorAll('tbody tr')
    for (var i = 0 ; i<cartItem.length; i++) {
        var inputValue = cartItem[i].querySelector('input')
        inputValue.addEventListener("change", function(){
            cartTotal();
        })    
    }
    
}
function changeTotalCart() {
    var cartItf = document.querySelector('#cart');
    var no_cart = document.querySelector('.no-product-cart')
    var lengthProduct = document.querySelectorAll('tbody tr')
    var content_length = document.querySelector('.leng-cart');
    var cart_interface = document.querySelector('#cart');
    content_length.innerHTML = lengthProduct.length;
    if (lengthProduct.length == 0) {
        cart_interface.style.display = 'none';
        content_length.style.display = 'none';
        no_cart.style.display = "block";
        
    } else {
        content_length.style.display = 'inline-block';
        // cart_interface.style.display = 'block';
        no_cart.style.display = "none";
        var btn_cart = document.querySelector('.header__cart');
        var no_cart = document.querySelector('.no-product-cart')
        btn_cart.addEventListener('click', function(){
            cart_interface.style.display = "block";
            no_cart.style.display = "none";

        })
    }
    // lấy ra nút X để tắt giỏ hàng
    var btn_X = document.querySelector('.icon-x');
    btn_X.addEventListener('click', function(){
        cart_interface.style.display = "none";
        no_cart.style.display = "none";
    })
}


// lấy nút button giỏ hàng , khi click vào giỏ hàng thì hiển thị chi tiết giỏ hàng
var btn_cart = document.querySelector('.header__cart');
var no_cart = document.querySelector('.no-product-cart')
btn_cart.addEventListener('click', function(){
    no_cart.style.display = "block";
})

// lấy ra nút X để tắt giỏ hàng
var btn_X = document.querySelector('.icon-x');
btn_X.addEventListener('click', function(){
    cart_interface.style.display = "none";
})
var btn_X_no_product = document.querySelector('.icon-x-no-cart');
btn_X_no_product.addEventListener('click', function(){
    no_cart.style.display = "none";
    cart_interface.display = "none"
})

// js chuyển giỏ hàng
// let btn_pay = document.querySelector('.pay');
// var cartItf1 = document.querySelector('#cart');
// btn_pay.addEventListener("click", function(){
    
// })


// js form thanh toán
const cityInput = document.querySelector('.province');
const cityBtn = document.querySelector('.cityBtn');
// Lấy danh sách các thành phố
// console.log(cityBtn)
const cityList = document.querySelector('.city-list');
const cities = cityList.getElementsByTagName('li');
// console.log(cities)

// Khi người dùng nhấn vào nút button
cityBtn.addEventListener('click', function() {
  // Hiển thị hoặc ẩn danh sách thành phố
  if (cityList.style.display === 'none') {
    cityList.style.display = 'block';
  } else {
    cityList.style.display = 'none';
  }
  
});

// Khi người dùng chọn một thành phố
for (let i = 0; i < cities.length; i++) {
  cities[i].addEventListener('click', function() {
    // Gán tên thành phố vào input
    cityInput.value = this.innerHTML;
    // Ẩn danh sách thành phố
    cityList.style.display = 'none';
    // console.log(cityInput.value)
  });
}

// js lấy huyện tương ứng
const districtSelect = document.getElementById("district");
for (let i = 0; i < cities.length; i++) {
  cities[i].addEventListener('click', function() {
    // Gán tên thành phố vào input
    cityInput.value = this.innerHTML;
    // Ẩn danh sách thành phố
    cityList.style.display = 'none';
    console.log(cityInput.value)

    let districtList = [];

switch (cityInput.value) {
  case "01 - Hà Nội":
    districtList = ["Ba Đình", "Hoàn Kiếm", "Đống Đa", "Cầu Giấy", "Hà Đông", "Hai Bà Trưng", "Hoàng Mai", "Thanh Xuân"];
    break;
  case "02 - Thành phố Hồ Chí Minh":
    districtList = ["Quận 1", "Quận 2", "Quận 3", "Quận 4", "Quận 5", "Quận 6", "Quận 7"];
    break;
  case "03 - Đà Nẵng":
    districtList = ["Quận Hải Châu", "Quận Thanh Khê", "Quận Sơn Trà", "Quận Cẩm Lệ", "Quận Hoàng Sa", "Quận Hòa Vang", "Quận Liên Chiểu"];
    break;
  case "04 - Hải Phòng":
    districtList = ["Quận Hồng Bàng", "Quận Lê Chân", "Quận Ngô Quyền", "Quận An Dương", "Quận An Lão", "Quận Bạch Long Vĩ"];
    break;
  case "05 - Hải Dương":
    districtList = ["Huyện Nam Sách", "Huyện Kim Thành", "Huyện Gia Lộc", "Huyện Bình Giang", "Huyện Cẩm Giàng", "Huyện Thanh Hà", "Huyện Thanh Miện", "Huyện Tứ Kỳ"];
    break;
  case "06 - Hưng yên":
    districtList = ["Thành phố Hưng Yên", "Thị xã Mỹ Hào", "Huyện Ân Thi", "Huyện Khoái Châu", "Huyện Kim Động", "Huyện Phù Cừ", "Huyện Văn Giang", "Huyện Văn Lâm" ];
    break;
  case "07 -Thanh Hóa":
    districtList = ["Quận Thanh Hóa", "Quận Nông Cống", "Quận Quảng Xương", "Thị xã Nghi Sơn", "Huyện Cẩm Thủy", "Huyện Hà Trung"];
    break;
  default:
    break;
}

// Xóa các option cũ trong thẻ select huyện
while (districtSelect.options.length > 0) {
  districtSelect.remove(0);
}

// Tạo các option mới tương ứng với danh sách các huyện
for (let i = 0; i < districtList.length; i++) {
  const option = document.createElement("option");
  option.value = districtList[i];
  option.text = districtList[i];
  districtSelect.add(option);
}
  });
}
// js lấy xã tương ứng
const wardsSelect = document.getElementById("district")
const wards = document.getElementById("wards")
wardsSelect.addEventListener("change", function(){
    console.log(wardsSelect.value)
    let wardsList = [];

switch (wardsSelect.value) {
    case "Ba Đình":
        wardsList = ["Xã Ba Đình 1", "Xã Ba Đình 2", "Xã Ba Đình 3", "Xã Ba Đình 4", "Xã Ba Đình 5", "Xã Ba Đình 6", "Xã Ba Đình 7", "Xã Ba Đình 8"];
        break;
  case "02 - Thành phố Hồ Chí Minh":
    wardsList = ["Quận 1", "Quận 2", "Quận 3", "Quận 4", "Quận 5", "Quận 6", "Quận 7"];
    break;
  case "03 - Đà Nẵng":
    wardsList = ["Quận Hải Châu", "Quận Thanh Khê", "Quận Sơn Trà", "Quận Cẩm Lệ", "Quận Hoàng Sa", "Quận Hòa Vang", "Quận Liên Chiểu"];
    break;
  case "04 - Hải Phòng":
    wardsList = ["Quận Hồng Bàng", "Quận Lê Chân", "Quận Ngô Quyền", "Quận An Dương", "Quận An Lão", "Quận Bạch Long Vĩ"];
    break;
  case "05 - Hải Dương":
    wardsList = ["Huyện Nam Sách", "Huyện Kim Thành", "Huyện Gia Lộc", "Huyện Bình Giang", "Huyện Cẩm Giàng", "Huyện Thanh Hà", "Huyện Thanh Miện", "Huyện Tứ Kỳ"];
    break;
    case "Huyện Khoái Châu":
        wardsList = ["Xã Dân Tiến", "Xã Hồng Tiến", "Xã Tân Dân", "Xã Đồng Tiến", "Xã Việt Hòa"];
        break;
  case "07 -Thanh Hóa":
    wardsList = ["Quận Thanh Hóa", "Quận Nông Cống", "Quận Quảng Xương", "Thị xã Nghi Sơn", "Huyện Cẩm Thủy", "Huyện Hà Trung"];
    break;
  default:
    break;
}
    while (wards.options.length > 0) {
        wards.remove(0);
     }
     // Tạo các option mới tương ứng với danh sách các xã
    for (let i = 0; i < wardsList.length; i++) {
        const option = document.createElement("option");
        option.value = wardsList[i];
        option.text = wardsList[i];
        wards.add(option);
  }

})



// js vadidator form

//KHỞI TẠO ĐỐI TƯỢNG VALIDATOR  
function Validator(options){
    // hàm validation  sử lý nút đăng ký
    //TẤT CẢ HÀM VALIDATOR ĐỀU ĐƯỢC ĐƯA QUA HÀM validator này
    function validator(inputElement, rule){
        //LẤY VALUE NGƯỜI DÙNG NHẬP VÀO  VALUE: inputElement.value, lấy ra test = rule.test
        var errorMessage = rule.test(inputElement.value);
        var errorElement = inputElement.parentElement.querySelector('.form-message');    
        if (errorMessage) {
            errorElement.innerText = errorMessage;
            inputElement.parentElement.classList.add('invalid')
        }
        else {
            errorElement.innerText =''
        }
        console.log(inputElement.value.length)
        // LẤY RA CÁC THẺ CHA TƯƠNG ỨNG VÀ LẤY THG THẺ SPAN CHỨ NỘI DUNG LỖI
        // console.log(inputElement.parentElement.querySelector('.form-message'))
    }

    var formElement = document.querySelector(options.form)
    console.log(formElement);
    if (formElement){
        //js submit
        formElement.onsubmit = function(e){
            e.preventDefault();
            // lặp qua các rule
            options.rules.forEach(function (rule){
                //lấy ra input element
                var inputElement = formElement.querySelector(rule.selector);
                //gọi hàm vadidator
                validator(inputElement, rule);
            })

        }
        //Lấy ra từng rules trong mảng rules
        options.rules.forEach(function (rule){
            var inputElement = formElement.querySelector(rule.selector);
            var errorElement = inputElement.parentElement.querySelector('.form-message');

            if (inputElement) {
                inputElement.onblur = function () {
                    validator(inputElement, rule);
                }
                //XỬ LÝ KHI NG DÙNG NHẬP THÌ XÓA DÒNG LỖI ĐI
                inputElement.oninput = function(){
                    errorElement.innerText ='';
                    inputElement.parentElement.classList.remove('invalid')
                }
            }          
        })
    }
    // console.log(options.rules)
}
// check họ tên
// Khi có lỗi trả ra message lỗi - khi 0 lỗi thì 0 trả ra gì cả
Validator.isRequired = function (selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.length > 50){
                return value= "Họ và tên nhập dưới 50 kí tự"        
            }else if (/\d/.test(value)){
                return value= "Họ và tên nhập không chứa số"   
            }else if (value.length == 0){
                return value= "Vui lòng nhập trường này"   
            }else if(value.length < 5){
                return value= "Họ và tên nhập trên 5 kí tự" 
            }
            else{
                return undefined
            }        
        }
        
    }
}
// check mail
Validator.isEmail = function(selector){
    return {
        selector: selector,
        test: function (value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if(regex.test(value)){
                return undefined
            }else if(value.length == 0){
                return value= "Vui lòng nhập trường này"
            }
            else{
                return value = "Email phải đúng định dạng gồm @ và ."
            }
        }
    };
}
// check sdt
Validator.isPhone = function(selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.length > 10){
                return value= "Vui lòng nhập số điện thoại dưới 11 kí tự !"
            }else if (/^[0-9]+$/.test(value) && value.length == 10){
                return undefined 
            }
            else if (value.length == 0){
                return value= "Vui lòng nhập trường này"
            }else if (value.length <= 9){
                return value= "Vui lòng nhập số điện thoại gồm 10 số !"
            }
            else{
                return value= "Số điện thoại chỉ gồm kí tự số"
            }         
        }
    }
}
Validator.isProvince = function(selector){
    return {
        selector: selector,
        test: function (value) {

            if (value.length == 0){
                return value= "Vui lòng chọn tỉnh dưới mũi tên!"      
            }else {
                return undefined
            }
    }
}
}
Validator.isDistrict = function(selector){
    return {
        selector: selector,
        test: function (value) {

            if (value.length == 0){
                return value= "Vui lòng chọn tỉnh!"      
            }else {
                return undefined
            }
    }
}
}
Validator.isWard = function(selector){
    return {
        selector: selector,
        test: function (value) {

            if (value.length == 0){
                return value= "Vui lòng chọn huyện!"      
            }else {
                return undefined
            }
    }
}
}

//check address
Validator.isAddress = function(selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.length > 40){
                return value= "Địa chỉ nhập dưới 40 kí tự"        
            }  
            else if (value.length == 0){
                return value= "Vui lòng nhập trường này"   
            }else if(value.length < 10){
                return value= "Vui lòng nhập đầy đủ địa chỉ"
            }
            else{
                return undefined
            }
        }
    }
}

//xử lí nút quay lại giỏ hàng
// lấy ra nút thanh toán
let btn_paySelect = document.querySelector('.btn-pay');
let btn_paysSelect = document.querySelector('.btn-pays');
let btn_cartSelect = document.querySelector('.btn-cart')
let cart_select = document.getElementById('cart');
let btn_cart_ = document.querySelector('.btn-cart')
let form_pay =document.querySelector('.form-pay')

console.log(cart_select);
btn_cart_.addEventListener("click", function(){
    btn_paySelect.style.display = 'none';
    form_pay.style.display = 'none'
    btn_paysSelect.style.display = 'inline-block';
    btn_cartSelect.style.display = 'none';
    cart_select.style.position = 'absolute';
    cart_select.style.height = '200vh'
    cart_select.style.width = '500px'
    cart_select.style.marginLeft = '1015px'
    cart_select.style.top = '0'
    cart_select.style.marginTop = '120px'
    btn_X.style.display = 'inline-block';
})
btn_paysSelect.addEventListener("click", function(){
    btn_X.style.display = 'none';
    btn_paysSelect.style.display = 'none';
    cart_select.style.position = 'relative';
    cart_select.style.height = '150vh'
    cart_select.style.width = '1200px'
    cart_select.style.margin = '0px 157px'
    cart_select.style.top = 'none'
    cart_select.style.paddingTop = '30px'
    form_pay.style.display = 'block'
    btn_paySelect.style.display = 'inline-block';
    btn_cart_.style.display = 'inline-block';
    btn_cart_.style.position = 'absolute';
    btn_cart_.style.marginTop = '-100px';


})
