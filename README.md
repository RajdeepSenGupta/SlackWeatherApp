# SlackWeatherApp

A simple demo of Slack INCOMING WEBHOOK and OUTGOING WEBHOOK, with OpenWeatherMap API

REQUIREMENT -
1. Setup ngRok -
	1. npm install ngRok
	2. Host your localhost application using ngRok - "ngrok http -host-header="localhost:58821" 58821"
2. Setup Outgoing Webhook (https://promact.slack.com/services/220622428070?updated=1) -
	1. Outgoing Webhook helps to call API from slack. 
	2. When slack encounters a specific trigger keyword, it calls the particular API.
	3. It automatically sends a payload object with the following parameters -
		a. token
		b. team_id
		c. team_domain
		d. channel_id
		e. channel_name
		f. timestamp
		g. user_id
		h. user_name
		i. text
		j. trigger_word
3. Setup Incoming Webhook (https://promact.slack.com/services/B6FMH14RG?added=1) -
	1. Incoming Webhook helps slack, to receive a message, sent from the API
	2. It receives a payload object sent from the API
4. Additional Settings -
	1. Go to StringConstant.cs
	2. Set Slack Incoming Webhook Link
	3. Set API Key of openweathermap
	4. The API of openweathermap is already given in the StringConstant

And we are good to go!!
