   /* URL encode / decode */
    var Url =
    {
        // public method for url encoding
        encode: function(string) {
            return escape(this._utf8_encode(string));
        },

        // public method for url decoding
        decode: function(string) {
            return this._utf8_decode(unescape(string));
        },

        // private method for UTF-8 encoding
        _utf8_encode: function(string) {
            string = string.replace(/\r\n/g, "\n");
            var utftext = "";

            for (var n = 0; n < string.length; n++) {

                var c = string.charCodeAt(n);

                if (c < 128) {
                    utftext += String.fromCharCode(c);
                }
                else if ((c > 127) && (c < 2048)) {
                    utftext += String.fromCharCode((c >> 6) | 192);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
                else {
                    utftext += String.fromCharCode((c >> 12) | 224);
                    utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                    utftext += String.fromCharCode((c & 63) | 128);
                }

            }

            return utftext;
        },

        // private method for UTF-8 decoding
        _utf8_decode: function(utftext) {
            var string = "";
            var i = 0;
            var c = c1 = c2 = 0;

            while (i < utftext.length) {

                c = utftext.charCodeAt(i);

                if (c < 128) {
                    string += String.fromCharCode(c);
                    i++;
                }
                else if ((c > 191) && (c < 224)) {
                    c2 = utftext.charCodeAt(i + 1);
                    string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                    i += 2;
                }
                else {
                    c2 = utftext.charCodeAt(i + 1);
                    c3 = utftext.charCodeAt(i + 2);
                    string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                    i += 3;
                }
            }
            return string;
        }
    }

    function searchEngine() 
    {
         var url = "Search.aspx?SearchValue=";
        if (document.getElementById("txtKeyword").value == '') 
        {
            alert('Bạn chưa gõ từ khóa tìm kiếm');
            return;
        }
        else
        {
            url +=Url.encode(document.getElementById("txtKeyword").value);
            window.open(url, "_self", "", "");
        }
    }
    function trapEnterKey(value, e) 
    {
        // the purpose of this function is to allow the enter key to
        // point to the correct button to click.
        var key;

        if (window.event) {
            // IE
            key = window.event.keyCode;
        }
        else {
            // firefox
            key = e.which;
        }

        if (key == 13) 
        {
            var q = value;

            if (q == '') {
                return false;
            }

            if ((q.indexOf('AND') == -1) && (q.indexOf('OR') == -1) && (q.indexOf('"') == -1)) {
                //q = '"' + q + '"';
                q = q;
            }
            
            // endcode
            q = Url.encode(q);
            
                var urltto = "Search.aspx?SearchValue=" + q;
                window.open(urltto, "_self", "", "");   
            
            event.keyCode = 0;

            return false;
        }
        
        return true;
    }                         

       