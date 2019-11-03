function initMap() {

    //let eventBox = document.getElementById("events");
    let mapBtns = document.getElementsByClassName("mapit");
    //var divToTheMap = document.getElementById('map');
    for (const btn of mapBtns) {

        btn.addEventListener('click', (e) => {
            e.preventDefault();
            let currentBtn = e.currentTarget;

            var address = { lat: -25.344, lng: 131.036 };


            console.log("vleznah u mtod");
            //var mapOptions = {
            //    center: address,
            //    zoom: 15,
            //    //minZoom: 15,
            //    //mapTypeId: google.maps.MapTypeId.ROADMAP
            //};
            
            var map = new google.maps.Map(
                document.getElementById('map'), {center: address , zoom: 8 });

            console.log(map);
            //console.log(map);
            ////console.log(divToTheMap);
            //console.log(document.getElementById('map'));
            var marker = new google.maps.Marker({ position: address});

            marker.setMap(map);


            currentBtn.innerHTML = "<i></i>Hide Map";

        });

    }
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
