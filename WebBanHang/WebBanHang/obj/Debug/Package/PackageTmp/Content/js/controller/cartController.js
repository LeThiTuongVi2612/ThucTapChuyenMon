var cart = {
    init: function () {
        cart.ergEvents();
    },
    ergEvents: function () {
        $('#btnCapNhatGH').on('click', function () {
            //kiểm tra sô lượng không phải là số  hoặc nhỏ hơn 0
            var SoLuong = $('.SoLuongThayDoi').val();
            if (isNaN(SoLuong) == true || SoLuong < 0) {

                $('#TB_SoLuongThayDoi').text("Số lượng không hợp lệ!");
                return false;
            }
        });

        $('#btnDangNhap').on('click', function () {
            $('#popup_login').show();
        });

        $('#close').click(AnForm);
        function AnForm() {
            $('#popup_login').hide();
        }
        $('#btnOK').on('click', function () {
            //duyệt vòng lập each

            var giatri = $('.TB_SL').val();
            if (isNaN(giatri) == true || giatri < 0) {
                alert("Số lượng không hợp lệ!");
                return false;
            }
            var giatri = $('.txtTB_DG').val();
            if (isNaN(giatri) == true || giatri < 0) {
                alert("Đơn giá không hợp lệ!");
                return false;
            }
        });
    }
}
cart.init();