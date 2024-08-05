let map;
let locations; // Declare locations variable outside the function

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    inforWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service is failed"
            : "Error: Your device doesn't support geolocation"
    );
    infoWindow.open(map);
}

function geocodeAddress(map, location) {
    //var geocoder = new google.maps.Geocoder();
    var content = "<h3>" + location.LocationName + "</h3><hr/><p>" + location.LocationAddress + "</p><hr/><p>" + location.Accepts + "</p>"
    var infoWindow = new google.maps.InfoWindow({ content: content });
    /*geocoder.geocode({ address: location.LocationAddress }, function (result, status) {
        if (status === "OK") {
            var marker = new google.maps.Marker({
                map: map,
                position: result[0].geometry.location
            });
            marker.addListener("click", function () {
                infoWindow.open(map, marker);
            })
        }
    });*/
    var latLng = new google.maps.LatLng(location.LocationLatitude, location.LocationLongitude);

    var marker = new google.maps.Marker({
        map: map,
        position: latLng
    });

    marker.addListener("click", function () {
        infoWindow.open(map, marker);
    });
}

function initMap() {
    /*const mapCenter = { lat: -37.8136, lng: 144.9631 }; // Melbourne CBD coordinates
    map = new google.maps.Map(document.getElementById("map"), {
        center: mapCenter,
        zoom: 11, // Adjust the zoom level as needed
    });

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            position => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                map.setCenter(pos);
            },
            () => {
                handleLocationError(false, infoWindow, map.getCenter());
            }
        );
    } else {
        handleLocationError(false, infoWindow, map.getCenter());
    }*/
    const mapCenter = { lat: -37.8136, lng: 144.9631 }; // Melbourne CBD coordinates
    map = new google.maps.Map(document.getElementById("map"), {
        center: mapCenter,
        zoom: 15, // Adjust the zoom level as needed
    });

    for (var i = 0; i < locations.length; i++) {
        console.log(locations[i]);
        geocodeAddress(map, locations[i]);
    }
}

// Make the XMLHttpRequest to get locations
let xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET", "/Locations/GetLocations", true);
xmlHttp.onreadystatechange = function () {
    if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
        locations = JSON.parse(xmlHttp.responseText); // Assign response to locations variable
        initMap(); // Call initMap after getting locations
    }
};
xmlHttp.send();


