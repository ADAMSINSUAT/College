$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build()

    connection.start()

    connection.on("getAllOrders", function(){

    })

    function loadData() {
        var tr = ""

        $.ajax({
            url: "api/GetOrder/",
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v)=> {
                    tr = tr + `<tr>
                    <td>${v.tempdeviceid}</td>
                    <td>${v.tempordernumber}</td>
                    <td>${v.tempinvoiceitem}</td>
                    <td>${v.tempinvoicequantity}</td>
                    <td>${v.temporderprice}</td>
                    <td>${v.temporderpriceamount}</td>
                    <td>${v.tempinvoicesubtotal}</td>
                    <td>${v.tempinvoicetime}</td>
                    <td>${v.tempinvoicemonth}</td>
                    <td>${v.tempinvoiceday}</td>
                    <td>${v.tempinvoiceyear}</td>
                    </tr>`
                })
                $("#tableBody").html(tr)
            },
            error: (error) => {
                console.log.error()
            }
        })
    }
})