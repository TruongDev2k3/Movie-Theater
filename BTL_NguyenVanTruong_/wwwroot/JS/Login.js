let eyeDisplay = document.querySelector('.btn_eye');
let eyeShow = document.querySelector('.btn_eyes');
let getform = document.querySelector('.form_login');
console.log(getform);

eyeDisplay.addEventListener("click", function(){
    var passwordField = document.getElementById("password");
    if (passwordField.type === "password") {
        passwordField.type = "text";
        eyeDisplay.style.display = 'none';
        eyeShow.style.display = 'inline';
      } else {
        passwordField.type = "password";
      }
})
eyeShow.addEventListener("click", function(){
    var passwordField = document.getElementById("password");
    if (passwordField.type === "text") {
        passwordField.type = "password";
        eyeDisplay.style.display = 'inline';
        eyeShow.style.display = 'none';
      } else {
        return undefined
      }
})

//KHỞI TẠO ĐỐI TƯỢNG VALIDATOR  
function Validator(options){
    // hàm validation  sử lý nút ĐĂNG NHẬP
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
    if (formElement){

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
// Khi có lỗi trả ra message lỗi - khi 0 lỗi thì 0 trả ra gì cả
Validator.username = function (selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.length > 20){
                return value= "Tên đăng nhập nhập dưới 20 kí tự"        
            }            
            else if (value.trim().length == 0){
                return value= "Vui lòng nhập trường này, không chứa dấu cách"   
            }else{
                return undefined
            }                    
        }      
    }
}
Validator.password = function (selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.length > 20){
                return value= "Mật khẩu phải dưới 20 kí tự"        
            }            
            else if (value.trim().length == 0){
                return value= "Vui lòng nhập trường này, không chứa dấu cách"   
            }else{
                return undefined
            }                    
        }      
    }
}
let getLinkLogin = document.querySelector('.login_link');
let getInputUser = document.querySelector('#username');
let getInputPassWord = document.querySelector('#password');
let getSubmitform = document.querySelector('.btn-register');
function checkInput(){
    const usernameValue = getInputUser.value.trim();
    const passwordValue = getInputPassWord.value.trim();
    if (usernameValue !== '' && passwordValue !== '') {
        getSubmitform.style.cursor = 'pointer';
        getSubmitform.style.opacity = '3';
        getLinkLogin.style.cursor = 'pointer';
      } else if(usernameValue == '' || passwordValue == '') {
        getSubmitform.style.cursor = 'not-allowed';
        getSubmitform.style.opacity = '.7';
        getLinkLogin.style.cursor = 'not-allowed';
      }
}

getInputUser.addEventListener('input', checkInput);
getInputPassWord.addEventListener('input', checkInput);

