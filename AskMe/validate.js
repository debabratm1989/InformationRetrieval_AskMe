$(document).ready(function() 
{
    
	$("#username").after("<span id='userNote'></span>");
	$("#password").after("<span id='passNote'></span>");
	$("#email").after("<span id='emailNote'></span>");


	$( "#username" ).focus(function() 
	{	
		$("#userNote").removeClass("error").removeClass("ok").addClass("info").show();
		$("#userNote").text("Enter Username");
	});

	$( "#username" ).blur(function() 
	{
		var inputUsername=$(this);
		var regUsername=/^[A-Za-z]+$/;
		if(inputUsername.val().length > 0)
		{
			var is_Username = regUsername.test(inputUsername.val());
			if(is_Username)
		{
			$("#userNote").removeClass("error").removeClass("info").addClass("ok").show();
			$("#userNote").text("OK");
		}
		else 
		{
			$("#userNote").removeClass("ok").removeClass("info").addClass("error").show();
			$("#userNote").text("Error");
		}	
		}

		else
		{
			$("#userNote").removeClass("ok").removeClass("info").removeClass("error").hide();
		}
		
	});

	$("#password").focus(function() 
	{
  
		$("#passNote").removeClass("error").removeClass("ok").addClass("info").show();
		$("#passNote").text("Enter Password");
	});

	$("#password").blur(function() 
	{
		var inputPass=$(this);
		//var re = /^[a-zA-Z0-9]/;
		var regPassword =/[a-zA-Z0-9]{8,}/;
		if(inputPass.val().length > 0)
		{
			var is_Pass= regPassword.test(inputPass.val());
			if(is_Pass)
		{
			$("#passNote").removeClass("error").removeClass("info").addClass("ok").show();
			$("#passNote").text("OK");
		}
		else 
		{
			$("#passNote").removeClass("ok").removeClass("info").addClass("error").show();
			$("#passNote").text("Error");
		}
		}


		else{
			$("#passNote").removeClass("ok").removeClass("info").removeClass("error").hide();
		}
		
		
	});
	
	$("#email").focus(function() 
	{
		$("#emailNote").removeClass("error").removeClass("ok").addClass("info").show();
		$("#emailNote").text("Enter Email");
	});

	$("#email").blur(function() 
	{
		var is_Email = false;
		var inputEmail=$(this);
		var regEmail= /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/ ;
		if(inputEmail.val().length > 0)
		{
			var is_Email = regEmail.test(inputEmail.val());

			if(!is_Email)
		{	$("#emailNote").removeClass("ok").removeClass("info").addClass("error").show();
			$("#emailNote").text("Error");
			
		}
		else 
		{
			$("#emailNote").removeClass("error").removeClass("info").addClass("ok").show();
			$("#emailNote").text("OK");
		}
		}
		else
		{
			$("#emailNote").removeClass("ok").removeClass("info").removeClass("error").hide();
		}
		
	});
	
});