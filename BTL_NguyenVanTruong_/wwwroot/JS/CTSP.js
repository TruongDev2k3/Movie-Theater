function displayImage(image) {
    var mainImage = document.querySelectorAll('.img_list');
    var imgshow = document.querySelector('.img-show');
    mainImage.src = image.src;
    imgshow.src = mainImage.src;
    console.log(mainImage.src)
  }
//   var mainImages = document.querySelectorAll('.img_list');
//   mainImages.addEventListener("onmouseout", function(){
//     mainImages.style.border = '2px solid red'
// })

const images = document.querySelectorAll('.img_list');
// js khi di vào ảnh lấy src gán vào src show
for (let i = 0; i < images.length; i++) {
  images[i].addEventListener('mouseover', function() {
    // loại bỏ class 'img-border' khỏi tất cả các ảnh
    for (let j = 0; j < images.length; j++) {
      images[j].classList.remove('img-border');
    }
    // thêm class 'img-border' vào ảnh được di chuột
    this.classList.add('img-border');
  });
}

  