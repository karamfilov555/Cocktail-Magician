function initialize() {
    var geocoder;
    var map;
    let mapBtns = document.getElementsByClassName("mapit");

    for (const btn of mapBtns) {

        btn.addEventListener('click', (e) => {

            e.preventDefault();
            let currentBtn = e.currentTarget;

            var divForTheMap = document.createElement('div');
            divForTheMap.style.height = '360px';
            divForTheMap.style.width = '98vm';
            divForTheMap.id = 'divForTheMap' + e.currentTarget.value;
            divForTheMap.style.display = 'block';
            divForTheMap.style.position = 'relative';
            divForTheMap.style.overflow = 'hidden';
            console.log(divForTheMap);

            if (document.getElementById("divForTheMap" + e.currentTarget.value) != null) {
                console.log("sega ima karta");
                if (document.getElementById("divForTheMap" + e.currentTarget.value).style.display == "none") {
                    document.getElementById("divForTheMap" + e.currentTarget.value).setAttribute("style", "display:block");
                    document.getElementById("divForTheMap" + e.currentTarget.value).style.width = '98vm'
                    document.getElementById("divForTheMap" + e.currentTarget.value).style.height = '360px'
                    document.getElementById("divForTheMap" + e.currentTarget.value).style.display = 'block'
                    document.getElementById("divForTheMap" + e.currentTarget.value).style.position = 'relative'
                    document.getElementById("divForTheMap" + e.currentTarget.value).style.overflow = 'hidden'
                    currentBtn.innerHTML = "<i></i>HIDE Map";
                    console.log("invisible");
                }
                else {

                    console.log("visible");
                    document.getElementById("divForTheMap" + e.currentTarget.value).setAttribute("style", "display:none");
                    currentBtn.innerHTML = "<i></i>Map it";
                }
            }
            else {
                //var divForTheMap = document.createElement('div');
                //divForTheMap.style.height = '360px';
                //divForTheMap.style.width = '98vm';
                //divForTheMap.id = 'divForTheMap' + e.currentTarget.value;
                //divForTheMap.style.display = 'block';
                //divForTheMap.style.position = 'relative';
                //divForTheMap.style.overflow = 'hidden';

                geocoder = new google.maps.Geocoder();
                var latlng = new google.maps.LatLng(-34.397, 150.644);

                var mapOptions = {
                    zoom: 8,
                    center: latlng
                }
                map = new google.maps.Map(divForTheMap, mapOptions);

                //geolocator
                var eventBox = e.currentTarget.parentElement;
                eventBox.parentNode.insertBefore(divForTheMap, eventBox.nextSibling);

                var address = e.currentTarget.value;
                //console.log(address); //button
                //console.log(e.currentTarget.value); //adress

                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == 'OK') {
                        map.setCenter(results[0].geometry.location);
                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location
                        });

                    } else {
                        alert('Geocode was not successful for the following reason: ' + status);
                    }
                });

                currentBtn.innerHTML = "<i></i>HIDE Map";
            }
        });
    }
}


function initialize2() {
    var geocoder;
    var map;
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var mapOptions = {
        zoom: 8,
        center: latlng
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);
    codeAddress(geocoder, map);
}

function codeAddress(geocoder, map) {

    var address = document.getElementById('address').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

//    function initMap() {

//        console.log("vleznah")
//                // The location
//                var adress = {lat: -25.344, lng: 131.036 };

//    var map = new google.maps.Map(
//                    document.getElementById('map'), {zoom: 5, center: adress });

//// The marker
//                var marker = new google.maps.Marker({position: adress, map: map });



//    <script async defer
//        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB1zn_ozE4kCyaqwMZDpQDEdR49e8S57To&callback=initMap">

//    </script>
//        <input type="button" value="Click to Display Map" onclick="DisplayGoogleMap()" />
//    <div id="myDiv" style="width:100%;height:400px;"></div>

//    <script type="text/javascript">
//        function DisplayGoogleMap() {

//                //Set the Latitude and Longitude of the Map
//                var myAddress = new google.maps.LatLng(24.466807, 54.384297);

//        //Create Options or set different Characteristics of Google Map
//                var mapOptions = {
//            center: myAddress,
//        zoom: 15,
//        minZoom: 15,
//        mapTypeId: google.maps.MapTypeId.ROADMAP
//    };

//    //Display the Google map in the div control with the defined Options
//    var map = new google.maps.Map(document.getElementById("myDiv"), mapOptions);

//    //Set Marker on the Map
//                var marker = new google.maps.Marker({
//            position: myAddress,
//        animation: google.maps.Animation.BOUNCE,
//    });

//    marker.setMap(map);
//}
//    </script>
//    <script>
//        Mapped();

//    </script>
