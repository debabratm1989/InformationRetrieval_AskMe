$(document).ready(function(){	
$("#Autom").append("<ul></ul>");	   
$.ajax({       
type: "get",    
 url: "Books.xml",    
 dataType: "xml",     
success: function(xml){ $(xml).find('book').each(function(){		
var title = $(this).find('title').text();		
//var author = $(this).find('author').text();		
var year = $(this).find('year').text();		
var price = $(this).find('price').text();		
var category = $(this).find('category').text();		
$("<li></li>").html(title + "-" + author).appendTo("#Autom ul");	
$('#people').append('<tr><td>' + title + '</td><td>' + author + '</td>'+ year + '</td>'+ price + '</td>'+ category + '</td></tr>');
   });		

}
    });
});