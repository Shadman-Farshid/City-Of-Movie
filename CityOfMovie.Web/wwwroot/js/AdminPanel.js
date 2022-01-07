

//$(document).ready(function () {

//    $.ajax({
//        url: "/AdminPanel/AdminPanel/jsonList",
//        method: "get"
//    }).done(function (result) {
//        var val = document.getElementById("userName").value;

//        var tbody = document.getElementById("tbody");

//        $.each(result.userList, function (index, value) {

//            var tr = document.createElement("tr");
//            var th = document.createElement("th");
//            var tdUsename = document.createElement("td");
//            var tdEmail = document.createElement("td");
//            var tdRegisterdate = document.createElement("td");
//            var tdactiv = document.createElement("td");
//            var tdBtn = document.createElement("td");
//            var divBtn = document.createElement("div");
//            var buttonEdit = document.createElement("button");
//            var buttonTresh = document.createElement("button");
//            var iEdit = document.createElement("i");
//            var iTresh = document.createElement("i");
//            var p = document.createElement("p");

//            th.setAttribute("scope", "row");
//            th.innerHTML = index + 1;
//            tr.appendChild(th);

//            tdUsename.setAttribute("valign", "middle");
//            tdUsename.innerHTML = value.userName;
//            tr.appendChild(tdUsename);

//            tdEmail.setAttribute("valign", "middle");
//            tdEmail.innerHTML = value.email;
//            tr.appendChild(tdEmail);

//            tdRegisterdate.setAttribute("valign", "middle");
//            tdRegisterdate.innerHTML = value.registerDate;
//            tr.appendChild(tdRegisterdate);

//            tdactiv.setAttribute("valign", "middle");
//            if (value.isActive) {
//                p.setAttribute("class", "text-success");
//                p.innerHTML = "فعال";
//            } else {
//                p.setAttribute("class", "text-danger");
//                p.innerHTML = "غیر فعال";
//            }
//            tdactiv.appendChild(p);
//            tr.appendChild(tdactiv);

//            tdBtn.setAttribute("valign", "middle");
//            divBtn.setAttribute("class", "table-buttons");
//            iEdit.setAttribute("class", "fa-solid fa-pen-to-square");
//            iTresh.setAttribute("class", "fa-solid fa-trash");
//            buttonEdit.appendChild(iEdit);
//            buttonTresh.appendChild(iTresh);
//            divBtn.appendChild(buttonEdit);
//            divBtn.appendChild(buttonTresh);
//            tdBtn.appendChild(divBtn);
//            tr.appendChild(tdBtn);



//            tbody.appendChild(tr);

//        });
//    })
//});



//function filterUserlist() {
//    var usernameInput = document.getElementById("userName").value;
//    var emailInput = document.getElementById("email").value;
//    $.ajax({
//        url: "/AdminPanel/AdminPanel/jsonList",
//        method: 'get',
//        data: { username: usernameInput, email: emailInput},
//    }).done(function (result) {

//        var tbody = document.getElementById("tbody");
//        tbody.innerHTML = "";
//        console.log(result);
//        $.each(result.userList, function (index, value) {

//            var tr = document.createElement("tr");
//            var th = document.createElement("th");
//            var tdUsename = document.createElement("td");
//            var tdEmail = document.createElement("td");
//            var tdRegisterdate = document.createElement("td");
//            var tdactiv = document.createElement("td");
//            var tdBtn = document.createElement("td");
//            var divBtn = document.createElement("div");
//            var buttonEdit = document.createElement("button");
//            var buttonTresh = document.createElement("button");
//            var iEdit = document.createElement("i");
//            var iTresh = document.createElement("i");
//            var p = document.createElement("p");

//            th.setAttribute("scope", "row");
//            th.innerHTML = index + 1;
//            tr.appendChild(th);

//            tdUsename.setAttribute("valign", "middle");
//            tdUsename.innerHTML = value.userName;
//            tr.appendChild(tdUsename);

//            tdEmail.setAttribute("valign", "middle");
//            tdEmail.innerHTML = value.email;
//            tr.appendChild(tdEmail);

//            tdRegisterdate.setAttribute("valign", "middle");
//            tdRegisterdate.innerHTML = value.registerDate;
//            tr.appendChild(tdRegisterdate);

//            tdactiv.setAttribute("valign", "middle");
//            if (value.isActive) {
//                p.setAttribute("class", "text-success");
//                p.innerHTML = "فعال";
//            } else {
//                p.setAttribute("class", "text-danger");
//                p.innerHTML = "غیر فعال";
//            }
//            tdactiv.appendChild(p);
//            tr.appendChild(tdactiv);

//            tdBtn.setAttribute("valign", "middle");
//            divBtn.setAttribute("class", "table-buttons");
//            iEdit.setAttribute("class", "fa-solid fa-pen-to-square");
//            iTresh.setAttribute("class", "fa-solid fa-trash");
//            buttonEdit.appendChild(iEdit);
//            buttonTresh.appendChild(iTresh);
//            divBtn.appendChild(buttonEdit);
//            divBtn.appendChild(buttonTresh);
//            tdBtn.appendChild(divBtn);
//            tr.appendChild(tdBtn);



//            tbody.appendChild(tr);

//        });
//    })
}