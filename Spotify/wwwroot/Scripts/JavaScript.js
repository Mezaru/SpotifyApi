function EnableValence() {
    let valence = $("#Valence");
    if (valence.prop("disabled")) {
        valence.prop("disabled", false);
        valence.css("opacity", "1");
    }
    else {
        valence.prop("disabled", true); 
        valence.css("opacity", "0.5");
    }
}