import { storage } from "./firebase";
import { getDownloadURL, ref, uploadBytesResumable } from "firebase/storage";
const Uploader = {
    async upload(files) {
      console.log(files);
      if (files == null) return;
      let storageRef;
      let urls;
      for (let i = 0; i < files.length; i++) {
        storageRef = ref(storage, `/images/${Date.now()}`);
        await uploadBytesResumable(storageRef, files[i]).then(async () => {
          const urlTask = await getDownloadURL(storageRef).then((url) => {
            // Remove common part from url to shorten it
            return url.replace(
              "https://firebasestorage.googleapis.com/v0/b/ecommerce-5ae64.appspot.com/o/images%",
              ""
            );
          });
          // Force Uploader to wait for url return
          Promise.resolve().then((urls = urls ? urls + " " + urlTask : urlTask));
        });
      }
  
      return urls;
    }
  };

export default Uploader;
