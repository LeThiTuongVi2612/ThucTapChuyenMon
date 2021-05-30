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
    }

}
cart.init();