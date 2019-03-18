/* ========================================================================
 * Scripts javascript for 
 * MyPOS
 * ======================================================================== */

/*
 * Scripts for
 * Transaction views
* ======================================================================== */

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
                    document.getElementById('GlobalTot').value = document.getElementById('subTotal1').value;
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
            document.getElementById('GlobalTot').value = subT;
        } else {
            var result = parseFloat(subT - (subT * d)).toFixed(2);
            document.getElementById('GlobalTot').value = result;
        }
    }
    catch (e) {
        document.getElementById('errorGlobalDiscount').textContent = e;
        document.getElementById('errorGlobalDiscount').style.visibility = "visible";
        console.log(e);
    }
}

function SearchByCodeOrName() {
    document.getElementById('errorSearchProduct').textContent = "";
    document.getElementById('errorSearchProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('searchProduct').value;
        if (val === null || val === "") {
            //alert("Il faut saisir un produit");
            throw "Il faut saisir un produit";

        } else {
            //var val = document.getElementById('searchProduct').value;

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    //document.getElementById('searchProduct').value = "";
                    document.getElementById('containerRight').innerHTML = xhr.responseText;
                }
            }

            //Post Method
            var url = "/Transaction/SearchProduct";
            var param = "Product=" + val;
            xhr.open("POST", url);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(param);
        }
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function AddItem(item) {
    document.getElementById('addProduct').value = item;
}

function SearchBy(method) {
    var val = method.getAttribute('Value');
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('containerRight').innerHTML = xhr.responseText;
        }
    }

    //Post Method
    var url = "/Transaction/SearchBy";
    var param = "Method=" + val;
    xhr.open("POST", url);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

function ProductBy(id, meth) {
    var val = id.getAttribute('Value');
    var method = meth;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('containerRight').innerHTML = xhr.responseText;
        }
    }

    //Post Method
    var url = "/Transaction/ProductBy";
    var param = "Method=" + method
        + "&Argument=" + val;
    xhr.open("POST", url);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

////remplacer par methodes controller
//function ButtonPayment_Click() {
//	var numTransaction = document.getElementById('NumTransaction').value;
//	var terminal = document.getElementById('TerminalId').value;
//	var vendor = document.getElementById('Vendor').value;
//	var discountG = document.getElementById('globalDiscount').value;
//	var vat = document.getElementById('GlobalVAT').value;
//	var total = document.getElementById('GlobalTotal').value;

//	var xhr = new XMLHttpRequest();
//	xhr.onreadystatechange = function () {
//		if (this.readyState == 4 && this.status == 200) {
//			document.getElementById('page').innerHTML = xhr.responseText;
//		}
//	}

//	//Post Method
//	var url = "/Transaction/Index";
//	var param = "numTransaction=" + numTransaction
//		+ "&terminalId=" + terminal
//		+ "&vendor=" + vendor
//		+ "&discountG=" + discountG
//		+ "&globalVAT=" + vat
//		+ "&globalTotal=" + total;
//	xhr.open("POST", url);
//	xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//	xhr.send(param);
//}

//function methodPayment(id) {
//    var val = id.getAttribute('Value');
//    var numTransaction = document.getElementById('NumTransaction').value;
//    var total = document.getElementById('GlobalTotal').value;

//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {
//            var choice = document.getElementById('paymentChoice');
//            choice.setAttribute("visibility", "visible");
//            choice.innerHTML = xhr.responseText;
//        }
//    }
//    //Post Method
//    var url = "/Pay/MethodChoice";
//    var param = "numTransaction=" + numTransaction
//        + "&globalTotal=" + total
//        + "&methodP=" + val;
//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}

//function AddCash() {
//    var cash = document.getElementById('cashReceived').value;
//    var total = document.getElementById('GlobalTotal').value;
//    total = parseFloat(total.replace(",", "."));
//    cash = parseFloat(cash.replace(",", "."));

//    if (cash > total) {
//        var result = parseFloat(cash - total).toFixed(2);
//        document.getElementById('cashReturn').value = result;
//        document.getElementById('cashReturn').style.borderColor = "orange";
//        document.getElementById('GlobalTotal').value = 0;
//        document.getElementById('GlobalTotal').style.borderColor = "green";
//    } else if (cash < total) {
//        var result = parseFloat(total - cash).toFixed(2);
//        document.getElementById('GlobalTotal').value = result;
//    } else {
//        document.getElementById('cashReturn').value = 0;
//        document.getElementById('cashReturn').style.borderColor = "green";
//        document.getElementById('GlobalTotal').value = 0;
//        document.getElementById('GlobalTotal').style.borderColor = "green";
//    }

//}

//function AskValidationCard() {
//    var total = document.getElementById('GlobalTotal').value;
//    var transac = document.getElementById('NumTransaction').value;
//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {

//            document.getElementById('paymentChoice').innerHTML = xhr.responseText;
//            var test = document.getElementById('vBagAmount').value;
//            document.getElementById('TTPay').style.visibility = "visible";
//            if (test === "0") {
//                document.getElementById('totalToPay').style.visibility = "hidden";
//                document.getElementById('vBagAmount').style.borderColor = "green";
//                document.getElementById('CashReturn').style.borderColor = "orange";
//            }
//        }
//    }
//    //Post Method
//    var url = "/Pay/PayCard";
//    var param = "numTransaction=" + transac
//        + "&amount=" + total;
//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}