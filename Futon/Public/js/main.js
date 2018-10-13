(function ($) {
    'use strict';
    
    $(".quick-view").click(function () {
        var id = $(this).data("id");
        var active = ""
        $.ajax({
            url: "/quickview/show/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
              
                if (response.status == 200) {
                    $(".modal-tab .product-details-large").empty()
                    for (var i = 0; i < response.data.listPhoto.length; i++) {
                       
                        var photo = ` <div class="tab-pane `+(i==0?"active":"")+`" id="image-`+(i+1)+`">
                                        <img src="/Uploads/Products/`+response.data.listPhoto[i]+`" alt="" />
                                    </div>
                                 `
                        var photoThumb = ` <a href="#image-` + (i + 1) + `"><img src="/Uploads/Products/Thumbnail/` + response.data.listPhoto[i] + `" alt="" /></a>`


                        $(".modal-tab .product-details-large").append(photo)
                        //$(".modal-tab .product-details-small .owl-stage").append(photoThumb)

                        //$(".modal-tab .product-details-small .owl-item a").attr("href", "#image-" + (i + 1))
                        //$(".modal-tab .product-details-small .owl-item a img").attr("src", "/Uploads/Products/Thumbnail/" + response.data.listPhoto[i])
                    }
                    $(".modal-pro-content .select-option-part select").empty();
                    $(".modal-pro-content .quickview-color-wrap ul").empty();
                    $(".modal-pro-content h3").text(response.data.name)
                    $(".modal-pro-content .price .product-price-old").text(response.data.price +"$")
                    $(".modal-pro-content .price span:last-child").text(response.data.discount + "$")
                    $(".modal-pro-content p").text(response.data.desc)
                    for (var i = 0; i < response.data.listsize.length; i++) {
                        var size = `<option value="` + response.data.listsize[i] + `">` + response.data.listsize[i] + `</option>`
                        $(".modal-pro-content .select-option-part select").append(size)
                    }
                    for (var i = 0; i < response.data.listColor.length; i++) {
                        var color = `  <li style = "background:` + response.data.listColor[i]+`"></li>`
                        $(".modal-pro-content .quickview-color-wrap ul").append(color)
                    }
                    if (response.data.status) {
                        $(".notstatus").addClass("deactive")
                    }
                    else {
                        $(".notstatus").removeClass("deactive")
                        $(".notstatus").addClass("active")
                        $(".status").addClass("deactive")
                    }
                }
            },
              error: function () {
                console.log("error");
            }

        })
    })
  
   

    $(".wishlist-cart").click(function (e) {

        e.preventDefault();
        var id = $(this).parents("tr").data("id");
        var tr = $(this).parents("tr")
        $.ajax({
            url: "/cart/add/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    updateCart(response.data);
                    swal("Product Added To Cart!", "", "success");
                    $(".cart-total h4").html('SUBTOTAL : ' + response.data.total + '$')
                    tr.remove();
                }
            },
            error: function () {
                console.log("error");
            }
        })
    })

    $(".wishlist").click(function (e) {
        
        e.preventDefault();
        var id = $(this).parents(".product-action").data("id");
        $.ajax({
            url: "/wishlist/add/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    console.log("ok")
                    
                }
            },
            error: function () {
                console.log("error");
            }
        })
    })

    $(".add-cart").click(function (e) {
        e.preventDefault();
        var id = $(this).parents(".product-action, .product-action-list, .product-quantity").data("id")
       
        
        $.ajax({
            url: "/cart/add/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    swal("Product Added To Cart!", "", "success");
                    updateCart(response.data);
                    $(".cart-total h4").html('SUBTOTAL : ' + response.data.total + '$')
                }
            },
            error: function () {
                console.log("error");
            }
        })
    });

    $(document).on("click", ".cart-delete a", function (e) {
        e.preventDefault();
        var id = $(this).parents("li").data("id")
        $.ajax({
            url:"/cart/remove/" + id ,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    updateCart(response.data);
                    $(".cart-total h4").html('SUBTOTAL : ' + response.data.total + '$')
                }
            },
            error: function () {
                console.log("error");
            }
        })
    })

    $(".cart-main-area .remove").click(function (e) {
        e.preventDefault();
        var tr = $(this).parents("tr");
        var id = $(this).parents("tr").data("id");
        $.ajax({
            url: "/cart/remove/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    swal("Product Removed", "", "error");
                    updateCart(response.data);
                    tr.remove();
                    $(".grand-total h5").text('Grand Total:   $'+response.data.total)
                }
            },
            error: function () {
                console.log("error");
            }
        })
    })

    $(".qtybut").on("input", function () {
       
        var value = parseInt($(this).val())

        if (value < 1) {
            $(this).val("1");
            value = 1;
        }
        var id = $(this).parents("tr").data("id")
        var tr = $(this).parents("tr")
        $.ajax({
            url: "/cart/changeqty/" + id + "?qty=" + value,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    updateCart(response.data);
                    tr.find(".subtotal").text('$' + response.data.subtotal);
                    $(".grand-total h5").text('Grand Total:   $' + response.data.total)
                }
            },
            error: function () {
                console.log("error");
            }
        })
    })


    

    function updateCart(data) {

        $(".cart-icon .digit").text('Cart: ' + data.count);
        $(".cart-icon .total").html('$' + data.total);

        $(".cart-content ul").empty();
       
        $.each(data.list, function (key, value) {

            var li = `<li class="single-cart" data-id="`+value.Id +`">
                <div class="cart-image d-flex">
                    <div class="img">
                        <a href="">
                            <img src="`+value.Photo+`" alt="">
                        </a>
                        <span class="count">`+value.Qty+`</span>
                    </div>
                </div>
                <div class="cart-title">
                    <h4>
                        <a href="">`+ value.Name +` </a>
                    </h4>
                    <span>`+ value.Price +`$</span>
                </div>
                <div class="cart-delete float-right">
                    <a href="">
                        <i class="fa fa-times"></i>
                    </a>
                </div>
            </li>`
            $(".cart-content ul").append(li)

        })
    }


    $(".submitRange").click(function () {
        var url = setGetParameter("range", $('#amount').val());
        window.location.href = url;
    });

    
    $(".limit").change(function () {
        var url = setGetParameter("limit", $(this).val());
        window.location.href = url;
    });


    function setGetParameter(paramName, paramValue) {
        var url = window.location.href;
        var hash = location.hash;
        url = url.replace(hash, '');
        if (url.indexOf(paramName + "=") >= 0) {
            var prefix = url.substring(0, url.indexOf(paramName));
            var suffix = url.substring(url.indexOf(paramName));
            suffix = suffix.substring(suffix.indexOf("=") + 1);
            suffix = (suffix.indexOf("&") >= 0) ? suffix.substring(suffix.indexOf("&")) : "";
            url = prefix + paramName + "=" + paramValue + suffix;
        }
        else {
            if (url.indexOf("?") < 0)
                url += "?" + paramName + "=" + paramValue;
            else
                url += "&" + paramName + "=" + paramValue;
        }
        return url + hash;
    }

})(jQuery);