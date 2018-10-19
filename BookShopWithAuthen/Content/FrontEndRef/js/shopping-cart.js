
function addMoreProductQuantityToCart(e, bookid) {
    let $ele = $(e.target).siblings('.txt-product-quantity');
    let val = $ele.val();
    let num = 1;
    if (checkPositiveNumber(val)) num = Math.floor(val) + 1;
    $ele.val(num);
    $.ajax({
        type: 'POST',
        url: 'Cart/changeQuantity',
        dataType: 'json',
        data: { bookID: bookid, quantity: num },
        success: function (data) {
            $("#id_totalPrice_" + bookid).html(data.totalPrice + ".000");
            $("#totalMoney").html(data.totalMoney + ".000");
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function minusProductQuantityToCart(e, bookid) {
    let $ele = $(e.target).siblings('.txt-product-quantity');
    let val = $ele.val();
    let num = 1;
    if (checkPositiveNumber(val) && Math.floor(val) > 1) num = Math.floor(val) - 1;
    $ele.val(num);
    $.ajax({
        type: 'POST',
        url: 'Cart/changeQuantity',
        dataType: 'json',
        data: { bookID: bookid, quantity: num },
        success: function (data) {
            $("#id_totalPrice_" + bookid).html(data.totalPrice + ".000");
            $("#totalMoney").html(data.totalMoney + ".000");
        },
        error: function (ex) {
            alert(ex);
        }
    });


}

function editQuantityOfCart(object,bookid) {
    $.ajax({
        type: 'POST',
        url: 'Cart/changeQuantity',
        dataType: 'json',
        data: { bookID: bookid, quantity: object.value },
        success: function (data) {
            $("#id_totalPrice_" + bookid).html(data.totalPrice + ".000");
            $("#totalMoney").html(data.totalMoney + ".000");
        },
        error: function (ex) {
            alert(ex);
        }
    });
}
function deleteItemFromCart(bookid) {
    $.ajax({
        type: 'POST',
        url: 'Cart/deleteItem',
        dataType: 'json',
        data: { bookID: bookid },
        success: function (data) {
            if (data.totalMoney == 0) {
                location.reload();
            }
            $("#totalMoney").html(data.totalMoney + ".000");
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function setDefaultVal(e) {
    let $ele = $(e.target);
    let val = $ele.val();
    if (!checkPositiveNumber(val)) $ele.val('1');
}

function removeCartProductItem(e) {
    let $ele = $(e.target).parents('tr');
    $ele.remove();
}

function convertNumTomoneyFormat(num) {
    if (isNaN(num)) return;
    let str = num + '';
    let l = str.length;
    return str.split('').reverse().reduce((acc, digit, index) => {
        if (index % 3 == 2 && index != l - 1) acc += digit + '.';
        else acc += digit;
    }, '').split('').reverse().join('');
}

function convertMoneyFormatToNumber(val) {
    return val.replace(/[.]/g, '');
}

function order() {
    $('.modal-order').modal('show');
}
//$('.btn-add-more-product-quantity').click(addMoreProductQuantityToCart);
$('.txt-product-quantity').on('input', setDefaultVal);
//$('.btn-minus-product-quantity').click(minusProductQuantityToCart);
$('.btn-remove-cart-product').click(removeCartProductItem);
$('.btn-order').click(order);
$('.btn-finish-order').click(() => {
    $('.modal-order').modal('hide');
    setTimeout(() => { alert('Thanh toan thanh cong, chung toi se giao hang trong 3 ngay toi') }, 200);
})

