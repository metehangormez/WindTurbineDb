// wwwroot/js/mapHelper.js

var mapInstance;

// Objede isimleri tam bilmediğimiz property'leri yakalamak için
function findKey(obj, containsList) {
    if (!obj) return null;
    const keys = Object.keys(obj);
    const loweredSearch = containsList.map(x => x.toLowerCase());

    for (const key of keys) {
        const lower = key.toLowerCase();
        if (loweredSearch.some(s => lower.includes(s))) {
            return key;
        }
    }
    return null;
}

function getNumber(obj, keysContains, defaultValue) {
    const key = findKey(obj, keysContains);
    if (!key) return defaultValue;
    const val = Number(obj[key]);
    return isNaN(val) ? defaultValue : val;
}

function getText(obj, keysContains, defaultValue) {
    const key = findKey(obj, keysContains);
    if (!key) return defaultValue;
    const val = obj[key];
    return (val === undefined || val === null) ? defaultValue : String(val);
}

window.initMap = (turbines) => {
    try {
        // Eski haritayı temizle
        if (mapInstance) {
            mapInstance.off();
            mapInstance.remove();
            mapInstance = null;
        }

        const defaultLat = 38.61;
        const defaultLon = 27.42;

        let centerLat = defaultLat;
        let centerLon = defaultLon;

        // Merkez için ilk türbinden lat/lon bulmaya çalış
        if (turbines && turbines.length > 0) {
            const t0 = turbines[0];

            const latCenterKey = findKey(t0, ["lat", "latitude"]);
            const lonCenterKey = findKey(t0, ["lon", "lng", "longitude"]);

            if (latCenterKey && lonCenterKey) {
                centerLat = Number(t0[latCenterKey]) || defaultLat;
                centerLon = Number(t0[lonCenterKey]) || defaultLon;
            }
        }

        const mapDiv = document.getElementById("map");
        if (!mapDiv) {
            console.error("map div bulunamadı.");
            return;
        }

        // Haritayı oluştur
        mapInstance = L.map("map").setView([centerLat, centerLon], 13);

        // OpenStreetMap zemin katmanı
        L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
            maxZoom: 19,
            attribution: "© OpenStreetMap"
        }).addTo(mapInstance);

        const points = [];

        // Türbin marker'ları
        if (turbines) {
            turbines.forEach(t => {
                // Her türbin için lat/lon key'lerini dinamik bul
                const latKey = findKey(t, ["lat", "latitude"]);
                const lonKey = findKey(t, ["lon", "lng", "longitude"]);

                if (!latKey || !lonKey) {
                    console.warn("Bu türbin için lat/lon bulunamadı, atlandı:", t);
                    return;
                }

                const lat = Number(t[latKey]);
                const lon = Number(t[lonKey]);

                if (isNaN(lat) || isNaN(lon)) {
                    console.warn("Lat/Lon sayı değil, türbin atlandı:", t);
                    return;
                }

                points.push([lat, lon]);

                let color = "#4caf50"; // yeşil
                const lastStatus = getText(t, ["status", "durum"], "");

                if (lastStatus === "Arızalı" || lastStatus === "5") color = "#f44336";
                else if (lastStatus === "Bakımda" || lastStatus === "4") color = "#ff9800";

                const powerValue = getNumber(t, ["power", "guc"], 0);
                const windValue = getNumber(t, ["wind", "ruzgar"], 0);

                const name = getText(t, ["name", "turbin", "turbine"], "");
                const model = getText(t, ["model", "tip"], "");

                const circle = L.circleMarker([lat, lon], {
                    color: color,
                    fillColor: color,
                    fillOpacity: 0.9,
                    radius: 12
                }).addTo(mapInstance);

                const popupContent = `
                    <div style="text-align:center;">
                        <h4 style="margin:0;">${name}</h4>
                        <p style="margin:5px 0;"><b>${model}</b></p>
                        <hr/>
                        <p>Durum: <b>${lastStatus}</b></p>
                        <p>Güç: ${powerValue.toFixed(2)} MW</p>
                        <p>Rüzgar: ${windValue.toFixed(1)} m/s</p>
                    </div>
                `;
                circle.bindPopup(popupContent);
            });
        }

        // TÜM TÜRBİNLERİ KADRAJA AL
        if (points.length > 1) {
            const bounds = L.latLngBounds(points);
            mapInstance.fitBounds(bounds, { padding: [50, 50] });
        } else if (points.length === 1) {
            mapInstance.setView(points[0], 15);
        }

        // Boyutları düzelt
        setTimeout(() => {
            mapInstance.invalidateSize();
        }, 300);
    } catch (e) {
        console.error("initMap içinde hata oluştu:", e);
    }
};
