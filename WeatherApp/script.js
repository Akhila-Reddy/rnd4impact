<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
  <meta http-equiv="Content-Style-Type" content="text/css">
  <title></title>
  <meta name="Generator" content="Cocoa HTML Writer">
  <meta name="CocoaVersion" content="2113.4">
  <style type="text/css">
    p.p1 {margin: 0.0px 0.0px 0.0px 0.0px; font: 17.3px Times; color: #000000; -webkit-text-stroke: #000000}
    p.p2 {margin: 0.0px 0.0px 0.0px 0.0px; font: 17.3px Times; color: #000000; -webkit-text-stroke: #000000; min-height: 20.0px}
    span.s1 {font-kerning: none}
  </style>
</head>
<body>
<p class="p1"><span class="s1">function getWeather() {</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>const apiKey = 'YOUR_OPENWEATHERMAP_API_KEY';</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>const locationInput = document.getElementById('location');</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>const location = locationInput.value;</span></p>
<p class="p2"><span class="s1"></span><br></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>// Check if the location is provided</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>if (!location) {</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>alert('Please enter a location.');</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>return;</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>}</span></p>
<p class="p2"><span class="s1"></span><br></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${location}&amp;appid=${apiKey}`;</span></p>
<p class="p2"><span class="s1"></span><br></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>fetch(apiUrl)</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>.then(response =&gt; response.json())</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>.then(data =&gt; {</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>// Update the HTML elements with the fetched data</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>const temperatureElement = document.getElementById('temperature');</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>const descriptionElement = document.getElementById('description');</span></p>
<p class="p2"><span class="s1"></span><br></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>temperatureElement.innerText = `Temperature: ${data.main.temp}°C`;</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>descriptionElement.innerText = `Description: ${data.weather[0].description}`;</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>})</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>.catch(error =&gt; {</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>console.error('Error fetching weather data:', error);</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">      </span>alert('Unable to fetch weather data. Please try again.');</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">    </span>});</span></p>
<p class="p2"><span class="s1"></span><br></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>// You can also fetch forecast data and update the forecast container similarly</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>// Use a different API endpoint for forecast, e.g., `https://api.openweathermap.org/data/2.5/forecast`</span></p>
<p class="p1"><span class="s1"><span class="Apple-converted-space">  </span>// Parse the forecast data and update the HTML accordingly</span></p>
<p class="p1"><span class="s1">}</span></p>
</body>
</html>
