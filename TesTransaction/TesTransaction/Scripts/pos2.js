
function ButtonCalc_Click(id) {
	var val = id.getAttribute('Value');
	document.getElementById('addProduct').value += val;
}

function ButtonDelete_Click() {
	var val = document.getElementById('addProduct').value;
	document.getElementById('addProduct').value = val.substring(0, val.length - 1);
}

function ButtonAddProduct_Click() {
	var minus = false;
	CreateRquestAddOrRemove(minus);
}

function ButtonRemoveProduct_Click() {
	var minus = true;
	CreateRquestAddOrRemove(minus);
}

function CreateRquestAddOrRemove(minus) {
	document.getElementById('errorAddProduct').textContent = "";
	document.getElementById('errorAddProduct').style.visibility = "hidden";
	try {
		var val = document.getElementById('addProduct').value;
		if (val === null || val === "") {
			throw "Il faut saisir un produit";
		} else {
			var val = document.getElementById('addProduct').value;
			var numTransaction = document.getElementById('NumTransaction').value;
			var terminal = document.getElementById('TerminalId').value;

			var xhr = new XMLHttpRequest();
			xhr.onreadystatechange = function () {
				if (this.readyState == 4 && this.status == 200) {
					document.getElementById('addProduct').value = "";
					document.getElementById('detail').innerHTML = xhr.responseText;
				}
			}

			//Post Method
			var url = "/Transaction/RefreshDetails";
			var param = "addProduct=" + val
				+ "&numTransaction=" + numTransaction
				+ "&terminalId=" + terminal
				+ "&minus=" + minus;
			xhr.open("POST", url);
			xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
			xhr.send(param);
		}
	} catch (e) {
		document.getElementById('errorAddProduct').textContent = e;
		document.getElementById('errorAddProduct').style.visibility = "visible";
		console.log(e);
	}
}
			
function AddDiscount() {
	document.getElementById('errorGlobalDiscount').textContent = "";
	document.getElementById('errorGlobalDiscount').style.visibility = "hidden";
	try {
		var subT = document.getElementById('subTotal1').value;
		subT = parseFloat(subT.replace(",", "."));
		var d = (parseFloat(document.getElementById('globalDiscount').value)) / 100;
		if (d < 0 || d > 1) {
			throw "valeur en % devant être comprise entre 0 et 100";	
		} else if (Number.isNaN(d) || d == undefined || d == null || d === "") {
			throw "valeur devant être un nombre entre 0 et 100";
		} else if (d == 0) {
			document.getElementById('subTotal2').value = subT;
		} else {
			var result = parseFloat(subT - (subT * d)).toFixed(2);
			document.getElementById('subTotal2').value = result;
		}
	}
	catch (e) {
		document.getElementById('errorGlobalDiscount').textContent = e;
		document.getElementById('errorGlobalDiscount').style.visibility = "visible";
		console.log(e);
	}
}

function AddVat() {
	document.getElementById('errorAddVat').textContent = "";
	document.getElementById('errorAddVat').style.visibility = "hidden";
	try {
		var v = document.getElementById('GlobalVAT').value;
		var subT = document.getElementById('subTotal2').value;
		//v = "0,21"  --> parseFloat(document.getElementById('listVats').value) ne marche pas
		//le parseFloat donne 0, souci avec la ","  il faut la remplacer par "."
		subT = parseFloat(subT.replace(",", "."));
		if (v === "" || v === "0,00" || v === null) {
			document.getElementById('GlobalTotal').value = subT;
		} else {
			v = parseFloat(v.replace(",", "."));
			var result = parseFloat(subT * ++v).toFixed(2);
			document.getElementById('GlobalTotal').value = result;
		}
	} catch (e) {
		document.getElementById('errorAddVat').textContent = e;
		document.getElementById('errorAddVat').style.visibility = "visible";
		console.log(e);
	}
}
			
function ButtonPayment_Click() {
	var numTransaction = document.getElementById('NumTransaction').value;
	var terminal = document.getElementById('TerminalId').value;
	var vendor = document.getElementById('Vendor').value;
	var discountG = document.getElementById('globalDiscount').value;
	var vat = document.getElementById('GlobalVAT').value;
	var total = document.getElementById('GlobalTotal').value;

	var xhr = new XMLHttpRequest();
	xhr.onreadystatechange = function () {
		if (this.readyState == 4 && this.status == 200) {
			document.getElementById('page').innerHTML = xhr.responseText;
		}
	}

	//Post Method
	var url = "/Transaction/Index";
	var param = "numTransaction=" + numTransaction
		+ "&terminalId=" + terminal
		+ "&vendor=" + vendor
		+ "&discountG=" + discountG
		+ "&globalVAT=" + vat
		+ "&globalTotal=" + total;
	xhr.open("POST", url);
	xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	xhr.send(param);
}
