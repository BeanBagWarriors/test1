require('dotenv').config();
const express = require('express');
const mongoose = require('mongoose');
const cors = require('cors');

const app = express();
const PORT = process.env.PORT || 4000;


app.use(express.json({ limit: '50mb' })); 
app.use(express.urlencoded({ limit: '50mb', extended: true }));
app.use((req, res, next) =>{
    console.log(req.path, req.method)
    if (req.url === '/' && req.method === 'GET') {
      res.writeHead(200, { 'Content-Type': 'text/plain' });
      res.end('Wassup amigo');
    }
    next();
 })

 app.post('/unity-data', (req, res) => {
    console.log("Date primite din Unity:", req.body);
    res.status(200).json({ message: "Success" });
});

app.get('/get-data', (req, res) => {
    const responseData = {
        serverMessage: "Salut de la server!",
        timestamp: new Date().toISOString()
    };
    res.status(200).json(responseData);
});

mongoose.connect(process.env.mongoDB)
.then(() => {
    console.log("MongoDB connected");
})
.catch((error) => {
    console.error("MongoDB connection failed:", error);
}); 

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
