REQUIREMENT
===========
1. Setup ngRok -
	1. npm install ngRok
	2. Host your localhost application using ngRok - "ngrok http -host-header="localhost:58821" 58821"
2. Setup Outgoing Webhook  -
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
		
3. Setup Incoming Webhook  -
	1. Incoming Webhook helps slack, to receive a message, sent from the API
	2. It receives a payload object sent from the API
4. Additional Settings -
	1. Go to StringConstant.cs
	2. Set Slack Incoming Webhook Link
	3. Set API Key of openweathermap
	4. The API of openweathermap is already given in the StringConstant

And we are good to go!!


HOW TO RUN 
==========
1. Add the webhooks to your channel
2. Set trigger word, for triggering the process
3. <trigger> <location_name>/<zip_code>/<area_id>
