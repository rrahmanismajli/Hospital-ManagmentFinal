var orderData = [
    { Month: 'Jan', Orders: 10 },
    { Month: 'Feb', Orders: 20 },
    { Month: 'Mar', Orders: 15 },
    { Month: 'Apr', Orders: 25 },
    // Add more data here
];
var ctx = document.getElementById('line-chart').getContext('2d');
var lineChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: orderData.map(d => d.Month),
        datasets: [{
            label: 'Orders',
            data: orderData.map(d => d.Orders),
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});