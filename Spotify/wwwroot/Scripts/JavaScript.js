function GetToken() {
	$.ajax(
		{
			method: "POST",
			url: "https://accounts.spotify.com/api/token",
			data: {
				"grant_type": "authorization_code",
				"redirect_uri": "http://localhost:60608/",
				"client_secret": "785d1417c7484e7997ebb7b896a76e6e",
				"client_id": "d8fdd6b51faf49e68dae7f508e4d36d1"
			},
			success: function (result) {
				console.log(result);
			}
		}
	);
}