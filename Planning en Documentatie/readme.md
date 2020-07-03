<h1>Handleiding applicatie</h1>
<p>Deze handleiding beschrijft de benodigdheden om de Cursus Administratie applicatie te draaien.</p>

<h2>Vereisten om de applicatie te starten:</h2>

<p>Om de applicatie te starten wordt verondersteld dat de volgende applicaties geïnstalleerd zijn:</p>
<ul>
	<li>
	Visual Studio: <a href="https://visualstudio.microsoft.com/downloads/">https://visualstudio.microsoft.com/downloads/</a>
	</li>
	<li>
	Visual Studio Code: <a href="https://code.visualstudio.com/download">https://code.visualstudio.com/download</a>
	</li>
	<li>
	SQL Server (Express): <a href="https://www.microsoft.com/nl-nl/sql-server/sql-server-downloads">https://www.microsoft.com/nl-nl/sql-server/sql-server-downloads</a>
	</li>
	<li>
	Node.js: <a href="https://nodejs.org/en/download/">https://nodejs.org/en/download/</a>
	</li>
</ul>


<h2>Backend</h2>
<p>Open de solution file <code>CASEMF\backend\backend\backend.sln</code>. Open vervolgens de package manager console (<i>Tools -> NuGet Package Manager -> Package Manager Console</i>) en voer het commando <code>Update-Database</code> uit. Dit commando zorgt er voor dat de database wordt gevuld met een aantal initiële cursussen.</p>
<p>Klik vervolgens in het menu op <i>Build</i> -> <i>Rebuild Solution</i>. Klik om de backend applicatie daadwerkelijk te starten op <i>IIS Express</i>. Als er een popup getoond wordt met de vraag of het SSL certificaat vertrouwd moet worden, klik dan op 'ja'. De gekozen browser opent, en zal na even wachten de text <i>Web-API draait</i> tonen. De backend applicatie is nu draaiende.</p>

<h2>Frontend</h2>
<p>Open de map <code>CASEMF\frontend</code> in Visual Studio Code als Administrator. Open een nieuwe terminal (<i>Terminal -> New Terminal</i>). Voer in deze terminal de volgende commando's uit en geef ze de tijd om uit te voeren:</p>

<ul>
	<li><code>npm install</code></li>
	<li><code>npm install -g @angular/cli</code></li>
	<li><code>Set-ExecutionPolicy RemoteSigned</code></li>
	<li><code>ng serve --open</code></li>
</ul>

<p>Het derde commando hoeft alleen uitgevoerd te worden als het laatste commando de foutmelding <a style="color:red;">ng: File cannot be loaded. The file is not digitally signed.</a> geeft. Na het uitvoeren van het laatste commando wordt het project gebuild, wat wel even kan duren, en wordt de frontend applicatie gestart en geopend. De applicatie kan nu gebruikt worden!</p>

<h2>Extra informatie</h2>

De applicatie is te benaderen op http://localhost:4200/<br>
Een lijst van cursussen in een gekozen week is te vinden op http://localhost:4200/overzicht<br>
De week kan gekozen worden vanuit de url, bijvoorbeeld http://localhost:4200/overzicht/2020/28, waarbij 2020 het jaar is en 28 het weeknummer<br>
Cursussen zijn toe te voegen via http://localhost:4200/toevoegen<br>

De Web-API is te benaderen op http://localhost:8080/<br>
Een rechtstreekse get request kan worden gedaan met http://localhost:8080/api/cursusinstantie/?weeknummer=28&jaar=2020
