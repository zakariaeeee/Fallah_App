
(function ($) {
 "use strict";
 
	/*----------------------
		Dialogs
	 -----------------------*/

	//Basic
	$('#sa-basic').on('click', function(){
		swal("Here's a message!");
	});

	//A title with a text under
	$('.sa-title').on('click', function () {
		var myVal = $(this).find('.data').data("value");
		var id = $(this).find("#idNot").val();
		var Action = $(this).attr("data-Action");
		swal({
			text: myVal
		}).then(function () {
			swal(Action, "L'action a bien passé", "success");
			window.location = '/Notification/selected?id=' + id;
		});
	});



	//Success Message
	$('#sa-success').on('click', function(){
		swal("Good job!", "Lorem ipsum dolor cry sit amet, consectetur adipiscing elit. Sed lorem erat, tincidunt vitae ipsum et, Spensaduran pellentesque maximus eniman. Mauriseleifend ex semper, lobortis purus.", "success")
	});

	//Warning Message for Refuser Demande----------
	$('.sa-warning').on('click', function () {

		var url = $(this).attr("data-href");
		var text = $(this).data("text");
		var Action = $(this).attr("data-Action");
		var cfrm = $(this).attr("data-confirm");
		swal({ 
			title: "Vous etes sur?",   
			text: text,   
			type: "warning",   
			showCancelButton: true,   
			confirmButtonText: cfrm,
		}).then(function () {
			
			swal(Action, "L'action a bien passe", "success");
			window.location = url;

		});
	});
	
	//Parameter
	$('#sa-params').on('click', function(){
		swal({   
			title: "Are you sure?",   
			text: "You will not be able to recover this imaginary file!",   
			type: "warning",   
			showCancelButton: true,   
			confirmButtonText: "Yes, delete it!",
			cancelButtonText: "No, cancel plx!",   
		}).then(function(isConfirm){
			if (isConfirm) {     
				swal("Deleted!", "Your imaginary file has been deleted.", "success");   
			} else {     
				swal("Cancelled", "Your imaginary file is safe :)", "error");   
			} 
		});
	});

	//Custom Image
	$('#sa-image').on('click', function(){
		swal({   
			title: "Sweet!",   
			text: "Here's a custom image.",   
			imageUrl: "img/dialog/like.png" 
		});
	});

	//Auto Close Timer
	$('#sa-close').on('click', function(){
		 swal({   
			title: "Auto close alert!",   
			text: "I will close in 2 seconds.",   
			timer: 2000
		});
	});

 
})(jQuery); 