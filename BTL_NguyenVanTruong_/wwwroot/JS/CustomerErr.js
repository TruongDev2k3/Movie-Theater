setTimeout(function() {
    $('.alert').alert('close');
})

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
             if (!formElement.querySelector('.invalid')) {
                formElement.submit();
            }
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

// let getInputUser = document.querySelector('#phone');
let getSubmitform = document.querySelector('#btn-create');

Validator.isPhone = function(selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.trim().length > 10 || value.trim().length <= 9){            
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                return value= "Vui lòng nhập số điện thoại hợp lệ gồm 10 kí tự !"
            }else if (/^[0-9]+$/.test(value.trim()) && value.trim().length == 10){  
                getSubmitform.style.cursor = 'pointer';
                getSubmitform.style.opacity = '3';
                return undefined;
            }
            else if (value.trim().length == 0){
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                return value= "Vui lòng nhập trường này !";
            }
            else{
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                return value= "Số điện thoại chỉ gồm kí tự số !"
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
                return value= "Vui lòng nhập trường này !"
            }
            else{
                return value = "Email phải đúng định dạng gồm @ và ."
            }
        }
    };
}

Validator.isFullName = function(selector){
    return {
        selector: selector,
        test: function (value) {

            if (value.length == 0){
                return value= "Vui lòng không để trống trường này !"      
            }else if(value.length < 5){
                return value= "Vui lòng nhập đầy đủ họ và tên !"  
            }else {
                return undefined
            }
    }
}
}

Validator.isGender = function(selector){
    return {
        selector: selector,
        test: function (value) {

            if (value.length == 0){
                return value= "Vui lòng không để trống trường này !"      
            }else {
                return undefined
            }
    }
}
}

Validator.isAddress = function(selector){
    return {
        selector: selector,
        test: function (value) {

            if (value.length == 0){
                return value= "Vui lòng không để trống trường này !"      
            }else {
                return undefined
            }
    }
}
}




