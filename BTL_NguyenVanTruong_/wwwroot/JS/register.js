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
let getBlueTick = document.querySelector('.blue_tick');
// let getInputUser = document.querySelector('#phone');
let getSubmitform = document.querySelector('.btn-register');
let getLinkLogin = document.querySelector('.login_link');
Validator.isPhone = function(selector){
    return {
        selector: selector,
        test: function (value) {
            if (value.trim().length > 10){
                getBlueTick.style.display = 'none';
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                getLinkLogin.style.cursor = 'not-allowed';
                return value= "Vui lòng nhập số điện thoại hợp lệ gồm 10 kí tự !"
            }else if (/^[0-9]+$/.test(value.trim()) && value.trim().length == 10){  
                getBlueTick.style.display = 'block' ;
                getSubmitform.style.cursor = 'pointer';
                getSubmitform.style.opacity = '3';
                getLinkLogin.style.cursor = 'pointer';
                return undefined;
            }
            else if (value.trim().length == 0){
                getBlueTick.style.display = 'none';
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                getLinkLogin.style.cursor = 'not-allowed';
                return value= "Vui lòng nhập trường này";
            }else if (value.trim().length <= 9){
                getBlueTick.style.display = 'none';
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                getLinkLogin.style.cursor = 'not-allowed';
                return value= "Vui lòng nhập số điện thoại hợp lệ !"
            }
            else{
                getBlueTick.style.display = 'none';
                getSubmitform.style.cursor = 'not-allowed';
                getSubmitform.style.opacity = '.7';
                getLinkLogin.style.cursor = 'not-allowed';
                return value= "Số điện thoại chỉ gồm kí tự số"
            }                      
        }
    }
}




