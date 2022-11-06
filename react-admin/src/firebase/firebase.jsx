// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getStorage } from "firebase/storage";

// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyBPGg067Er2uUXb27sCzjBAnxYpShQOEHQ",
  authDomain: "ecommerce-5ae64.firebaseapp.com",
  projectId: "ecommerce-5ae64",
  storageBucket: "ecommerce-5ae64.appspot.com",
  messagingSenderId: "838078942486",
  appId: "1:838078942486:web:e9f321e5e1281355115437",
  measurementId: "G-DWHVEWBS8Z"
};

// Initialize Firebase
// const app = initializeApp(firebaseConfig);
// const analytics = getAnalytics(app);

export const app = initializeApp(firebaseConfig);
export const storage = getStorage(app);