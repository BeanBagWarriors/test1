import { Unity, useUnityContext } from "react-unity-webgl";
import { useEffect } from "react";

function App() {
  // Extragem sendMessage din hook
  const { unityProvider, isLoaded, sendMessage } = useUnityContext({
    loaderUrl: "/Loaders/Build/Loaders.loader.js",
    dataUrl: "/Loaders/Build/Loaders.data.unityweb",
    frameworkUrl: "/Loaders/Build/Loaders.framework.js.unityweb",
    codeUrl: "/Loaders/Build/Loaders.wasm.unityweb",
  });

  const dataToSend = {
    name: "John Doe",
    score: 42,
    level: 3,
  };

  const sendDataToUnity = () => {
    const jsonData = JSON.stringify(dataToSend);
    console.log("Trimit datele:", jsonData);
    // Folosim sendMessage extras din hook
    sendMessage("Player", "ReceiveData", jsonData);
  };

  useEffect(() => {
    if (isLoaded) {
      console.log("Unity este încărcat");
      sendDataToUnity();
    }
  }, [isLoaded]);

  return (
    <div className="App">
      <div>
        <Unity 
          unityProvider={unityProvider} 
          style={{ width: "100%", height: "90%" }}
        />
      </div>
    </div>
  );
}

export default App;
