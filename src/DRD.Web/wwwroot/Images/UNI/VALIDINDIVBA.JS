mess1=""
mess2=""
mess3=""
mess4=""
mess5=""

function chklnam()
{
  var cha=document.phform.elements[26].value
  var lena=cha.length
  var lencha=lena
  if (lencha>30)
     {
      mess1="ERROR (**LAST NAME) : The LAST NAME should not be more than 30 characters"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[26].focus()
      window.document.phform.elements[26].select()
     }
  else
     {
      mess1=""
      message=mess1+mess2+mess3+mess4+mess5
     }
}

function chkfnam(){
  var chb=document.phform.elements[27].value
  var lenb=chb.length
  var lenchb=lenb
  if (lenchb>30)
     {
      mess2="ERROR (**FIRST NAME) : The FISRT NAME should not be more than 30 characters"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[27].focus()
      window.document.phform.elements[27].select()
     }
  else
     {
      mess2=""
      message=mess1+mess2+mess3+mess4+mess5     	
     }
  
}


function chkaddra()

{
  var chc=document.phform.elements[28].value
  var lenc=chc.length
  var lenchc=lenc
  if (lenchc>30)
     {
      mess3="ERROR (**ADDRESS) : The ADDRESS should not be more than 30 characters"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[28].focus()
      window.document.phform.elements[28].select()
     }
  else
     {
      mess3=""
      message=mess1+mess2+mess3+mess4+mess5      	
     }
  
}


function chkstreet()

{
  var chd=document.phform.elements[29].value
  var lend=chd.length
  var lenchd=lend
  if (lenchd>30)
     {
      mess4="ERROR (**STREET) : The STREET should not be more than 30 characters"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[29].focus()
      window.document.phform.elements[29].select()
     }
  else
     {
      mess4=""
      message=mess1+mess2+mess3+mess4+mess5      	    	
     }
  
}

function chkcity()

{
  var che=document.phform.elements[30].value
  var lene=che.length
  var lenche=lene
  if (lenche>30)
     {
      mess5="**ERROR (**CITY) : The CITY should not be more than 30 characters"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[30].focus()
      window.document.phform.elements[30].select()
     }
  else
     {
      mess5=""
      message=mess1+mess2+mess3+mess4+mess5      	    	
     }
}

