var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#frmPayment').validate({
            rules: {
                name: 'required',
                address: 'required',
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: 'Bạn phải nhập họ tên.',
                address: 'Bạn phải nhập địa chỉ',
                email: {
                    required: 'Bạn phải nhập email',
                    email: 'Định dạng email không đúng.'
                },
                phone: {
                    required: 'Bạn phải nhập số điện thoại',
                    number: 'Số điện thoại phải là số'
                }
            }
        });
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
                cart.updateAll();
            }
            else {
                $('#amount_' + productId).text(0);
            }

            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
            
        });
        $('.txtQuantity').off('change').on('change', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
                cart.updateAll();
            }
            else {
                $('#amount_' + productId).text(0);
            }

            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));

        });
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });
        $('#chkUserLoginInfo').off('click').on('click', function (e) {
            if ($(this).prop('checked')) {
                cart.getLoginUser();
            } else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
            
        });
        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }
        });
    },
    getLoginUser: function() {
        $.ajax({
            url: '/shoppingCart/GetUser',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var user = res.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: 'Thanh toán tiền mặt',
            PaymentStatus: false,
        };
        $.ajax({
            url: '/shoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (res) {
                if (res.status) {
                    $('#divCheckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('#cartContent').html('<div class="alert alert-info alert-dismissible" role="alert"> ' +
                                                  '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button> ' +
                                                          'Cám ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ đến bạn sớm nhất. ' +
                                              '</div>');
                    }, 2000);
                }
            }
        });
    },
    getTotalOrder: function() {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });

        if (isNaN(total)) {
            return 0;
        }

        return total;
    },
    addItem: function(productId) {
        $.ajax({
            url: '/shoppingCart/Add',
            type: 'POST',
            data: { productId: productId },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    alert("Thêm vào giỏ hàng thành công.")
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: '/shoppingCart/DeleteItem',
            type: 'POST',
            data: { productId: productId },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    updateAll: function () {
        var cartList = [];
        var listTextBox = $('.txtQuantity');
        $.each(listTextBox, function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            })
        });
        $.ajax({
            url: '/shoppingCart/Update',
            type: 'POST',
            data: { cartData: JSON.stringify(cartList)  },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                    console.log('Updated successfully');
                }
            }
        });
    },
    loadData: function () {
        var template = $('#tplCart').html();
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status && template !== undefined && template !== null) {
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            Stt: i + 1,
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    })

                    if (html === '') {
                        $('#cartContent').html('<div class="alert alert-info alert-dismissible" role="alert"> ' +
                                                    '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button> ' +
                                                            ' Giỏ hàng bạn không có sản phẩm nào. ' +
                                                '</div>')
                    };

                    $('#cartBody').html(html);
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        })
    },
    deleteAll: function() {
        $.ajax({
            url: '/shoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
}

cart.init();