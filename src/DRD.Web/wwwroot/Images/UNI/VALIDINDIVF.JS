mess1=""
mess2=""
mess3=""
mess4=""
mess5=""

function chknom()
{
  var cha=document.phform.elements[24].value
  var lena=cha.length
  var lencha=lena
  if (lencha>30)
     {
      mess1="ERREUR (**NOM) : Le NOM ne doit pas dépasser 30 caractères"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[24].focus()
      window.document.phform.elements[24].select()
     }
  else
     {
      mess1=""
      message=mess1+mess2+mess3+mess4+mess5
     }
}


function chkpnom(){
  var chb=document.phform.elements[25].value
  var lenb=chb.length
  var lenchb=lenb
  if (lenchb>30)
     {
      mess2="ERREUR (**PRENOM) : Le PRENOM ne doit pas dépasser 30 caractères"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[25].focus()
      window.document.phform.elements[25].select()
     }
  else
     {
      mess2=""
      message=mess1+mess2+mess3+mess4+mess5     	
     }
  
}


function chkaddr()

{
  var chc=document.phform.elements[26].value
  var lenc=chc.length
  var lenchc=lenc
  if (lenchc>30)
     {
      mess3="ERREUR (**ADRESSE) : L'ADRESSE ne doit pas dépasser 30 caractères"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[26].focus()
      window.document.phform.elements[26].select()
     }
  else
     {
      mess3=""
      message=mess1+mess2+mess3+mess4+mess5      	
     }
  
}

function chkrue()

{
  var chd=document.phform.elements[27].value
  var lend=chd.length
  var lenchd=lend
  if (lenchd>30)
     {
      mess4="ERREUR (**RUE) : Le nom de RUE ne doit pas dépasser 30 caractères"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[27].focus()
      window.document.phform.elements[27].select()
     }
  else
     {
      mess4=""
      message=mess1+mess2+mess3+mess4+mess5      	    	
     }
  
}

function chkville()

{
  var che=document.phform.elements[28].value
  var lene=che.length
  var lenche=lene
  if (lenche>30)
     {
      mess5="**ERREUR (**VILLE) : Le nom de la VILLE ne doit pas dépasser 30 caractères"
      message=mess1+mess2+mess3+mess4+mess5
      alert(message)
      window.document.phform.elements[28].focus()
      window.document.phform.elements[28].select()
     }
  else
     {
      mess5=""
      message=mess1+mess2+mess3+mess4+mess5      	    	
     }
}

