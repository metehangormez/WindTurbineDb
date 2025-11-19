var mapInstance;

window.initMap = (turbines) => {

    if (mapInstance) {
        mapInstance.remove();
    }

    // Varsayılan Merkez: Manisa
    var centerLat = 38.61;
    var centerLon = 27.42;

    if (turbines && turbines.length > 0) {
        centerLat = turbines[0].latitude;
        centerLon = turbines[0].longitude;
    }

    mapInstance = L.map('map').setView([centerLat, centerLon], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap'
    }).addTo(mapInstance);

    turbines.forEach(t => {
        var color = '#4caf50'; // Yeşil
        if (t.lastStatus === 'Arızalı' || t.lastStatus === '5') color = '#f44336'; // Kırmızı
        else if (t.lastStatus === 'Bakımda' || t.lastStatus === '4') color = '#ff9800'; // Turuncu

        var circle = L.circleMarker([t.latitude, t.longitude], {
            color: color,
            fillColor: color,
            fillOpacity: 0.8,
            radius: 12
        }).addTo(mapInstance);

        var popupContent = `
            <div style="text-align:center;">
                <h4 style="margin:0;">${t.name}</h4>
                <p style="margin:5px 0;"><b>${t.model}</b></p>
                <hr/>
                <p>Durum: <b>${t.lastStatus}</b></p>
                <p>Güç: ${t.currentPower.toFixed(2)} MW</p>
                <p>Rüzgar: ${t.currentWindSpeed.toFixed(1)} m/s</p>
            </div>
        `;
        circle.bindPopup(popupContent);
    });
}