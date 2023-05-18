var url = window.location.href;
var tokens = url.split("?");;
var token = tokens[1];
   axios.get('https://localhost:7126/api/LinkApi?token='+token).then(function (response) {

    window.location.href =response.data;
  });





const getbtn= document.getElementById('getbtn').value;
const urlPlace = document.getElementById('urlPlace');
const urlShort = document.getElementById('shortUrl');

async function getText() {
    var inputText = document.getElementById("getbtn").value;

const valuegetbtnJson = 
{orginalUrl :inputText};

    try {
     
urlPlace.innerHTML = '';

  
      const WaitDiv = document.createElement('div');
const WaitDivText = ' <div class="alert alert-warning" id="shortUrl">Please Wait ... </div>';
WaitDiv.innerHTML = WaitDivText;
urlPlace.appendChild(WaitDiv);
      const response = await axios.post('https://ShortLinkapi.bashiridev.ir/api/LinkApi/',valuegetbtnJson );
const x =response.data.value;
urlPlace.removeChild(WaitDiv) ;
const linkDiv = document.createElement('div');

const linkDivText = ' <div class="alert alert-success" id="shortUrl"> https://ShortLinkapi.bashiridev.ir/'+ x.substr(24); +'</div>';
linkDiv.innerHTML = linkDivText;
urlPlace.appendChild(linkDiv);
    } catch (error) {
      console.error(error);
    }

  }
