
var connection = new signalR.HubConnectionBuilder().withUrl("https://locationapi.imaginewebsite.com.tr/locationhub").build();

connection.on("ReceiveLocation", (latitude, longitude, name, userid, phonenumber,speed ) => {

        markers[userid].setLatLng([latitude, longitude]);
    markers[userid].bindPopup(`<b>Ad: ${name.toUpperCase()} <br><br>Numara: ${phonenumber} <br><br> Hız: ${parseInt(speed)} km/h`, { autoPan: false }).openPopup();
});

connection.on("ReceiveNewUser", (latitude, longitude, name, userid, phonenumber) => {
    const marker = L.marker([latitude, longitude]);

    marker.bindPopup(`<b>Ad: ${name.toUpperCase()} <br><br> ${phonenumber} `, {
        autoPan: false
    }).openPopup();
    marker.addTo(map);

    markers[userid] = marker;
})

connection.on("ReceiveUserRemoval", (id) => {
    console.log("worked");
    const marker = markers[id];
    if (marker) {
        map.removeLayer(marker);
        delete markers[id];
    }
    else {
        console.log("marker could not be found for the user");
    }
});

connection.start().then(function () {
    console.log("started");
}).catch(function (err) {
    console.log("connect failed");
});