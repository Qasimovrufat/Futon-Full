$(document).ready(function () {


    $(".photo").change(function () {
        var input = this;
        var url = $(this).val();
        var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#img').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
        else {
            $('#img').attr('src', '/assets/no_preview.png');
        }
        })       
    

    $(".select2").select2();

    $("select[name='DepartmentId']").change(function () {
        $.getJSON("/manage/products/categories/" + $(this).val(), function (response) {

            if (response.status == 200) {
                $("select[name='CategoryId']").empty();
                $("select[name='CategoryId']").removeClass("disabled");
                $.each(response.data, function (key, value) {
                
                    var opt = $("<option></option>");
                    opt.text(value.Name);
                    opt.val(value.Id)
                    $("select[name='CategoryId']").append(opt);
                })
            }
        })
    })

    ClassicEditor.create(document.querySelector('#editor'))
        .then(editor => {
            theEditor = editor;
        })
        .catch(error => {
            console.error(error);
        });
  
    $("#Upload").change(function () {
      
        var form_data = new FormData();
        var ins = this.files.length;

        for (var x = 0; x < ins; x++) {
            form_data.append("files[]", this.files[x]);
        }

        $.ajax({
            url: "/manage/products/upload",
            type: "post",
            dataType: "json",
            data: form_data,
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                for (var i = 0; i < response.data.length; i++) {
                    var item = `<li>
                                    <img src="`+ response.data[i].url + `"/>
                                    <a class="remove" data-file='`+ response.data[i].filename + `' href="#"><i class="fas fa-times"></i></a>
                                </li>`;
                    $(".photos").append(item);
                }
            }
        })
    });

    $(document).on("click", ".photos .remove", function (ev) {
        ev.preventDefault();
        $this = $(this);
        var filename = $(this).data("file");
        $.ajax({
            url: "/manage/products/removefile?filename=" + filename,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response.status == 200) {
                    $this.parent().remove();
                }
            }
        });
    });

    $(".CreateProduct").click(function () {
        var content = $(".ck-content p").text();
        
        var info = $("#infoForm").serializeArray();
        
        var data  ={ };
        for (var i = 0; i < info.length; i++) {
            data[info[i].name] = info[i].value;
        }
        data.Description = [];
        data.Description.push(content)
        data.Photos = [];

        $(".photos li").each(function (index) {
            var pth = {
                OrderBy : index + 1,
                Path: $(this).find("a").data("file"),
            }
            data.Photos.push(pth);
        });


        var tags = $('#Tags').select2('val');
        data.ProductTags = [];

        for (var i = 0; i < tags.length; i++) {
            var tg = {
                TagId: tags[i]
            }
            data.ProductTags.push(tg);
        }

        var colors = $("#Colors").select2('val');
        var sizes = $("#Sizes").select2('val');
        var cs = {}
        data.ProductColorSizes = [];
        for (var i = 0; i < colors.length; i++) {
            cs = {
                ColorId: colors[i],
            }
           data.ProductColorSizes.push(cs)
        }
        for (var i = 0; i < sizes.length; i++) {
            cs = {
                SizeId: sizes[i],
            }
            data.ProductColorSizes.push(cs)
        }


        console.log(data)
        $.ajax({
            url:" /manage/products/create",
            type: "post",
            dataType: "json",
            data :data,
            success: function (response) {
                if (response.status == 200) {
                    var getUrl = window.location;
                    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];
                    window.location.href = baseUrl + "/products";
                } else {
                    toastr.error(response.message)
                }
              
            }

        })
    })

    $(".StatusToggle").click(function () {
        console.log("sdasd")
        $this = $(this);
        $.ajax({
            url: "/manage/products/togglestatus/" + $(this).parents("tr").data("id") + "?status=" + $(this).data("val"),
            type: "get",
            dataType: "json",
            success: function (response) {
                if ($this.data("val") == true) {
                    $this.data("val", false);
                    $this.removeClass("badge-danger").addClass("badge-success").text("Active");
                } else {
                    $this.data("val", true);
                    $this.removeClass("badge-success").addClass("badge-danger").text("Deactive");
                }
            }
        })

    })

    $(".EditProduct").click(function () {
        var content = $(".ck-content p").text();
        var ProductId = $("#infoForm").data("id");
        var info = $("#infoForm").serializeArray();

        var data = {};
        for (var i = 0; i < info.length; i++) {
            data[info[i].name] = info[i].value;
        }
        data["Description"] = content;
     
        data.Images = [];

        $(".photos li").each(function (index) {
            var pth = {
                ProductId: ProductId,
                OrderBy: index + 1,
                Path: $(this).find("a").data("file"),
            }
            data.Images.push(pth);
        });


        var tags = $('#Tags').select2('val');
        data.Tags = [];

        for (var i = 0; i < tags.length; i++) {
            var tg = {
                ProductId: ProductId,
                TagId: tags[i]
            }
            data.Tags.push(tg);
        }

        var colors = $("#Colors").select2('val');
        var sizes = $("#Sizes").select2('val');
        var cs = {}
        data.ColorSize = [];
        for (var i = 0; i < colors.length; i++) {
            cs = {
                ProductId: ProductId,
                ColorId: colors[i],
            }
            data.ColorSize.push(cs)
        }
        for (var i = 0; i < sizes.length; i++) {
            cs = {
                ProductId: ProductId,
                SizeId: sizes[i],
            }
            data.ColorSize.push(cs)
        }


        console.log(data)
        $.ajax({
            url: $(".infoform").attr("action"),
            type: "post",
            dataType: "json",
            data: data,
            success: function (response) {
                if (response.status == 200) {
                    var getUrl = window.location;
                    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];
                    window.location.href = baseUrl + "/products";
                } else {
                    toastr.error(response.message)
                }

            }

        })
    })
})