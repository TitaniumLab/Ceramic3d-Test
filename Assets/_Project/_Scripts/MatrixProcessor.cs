using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ceramic3dTest
{
    public class MatrixProcessor : MonoBehaviour
    {
        [SerializeField] private TextAsset _modelJson;
        [SerializeField] private TextAsset _spaceJson;
        [SerializeField] private MeshRenderer _prefab;
        private MeshRenderer[] _mRs;

        private void Awake()
        {
            // Get data from Json
            DataConverter model = JsonUtility.FromJson<DataConverter>(_modelJson.text);
            DataConverter space = JsonUtility.FromJson<DataConverter>(_spaceJson.text);
            Matrix4x4[] modelMatrixs = model.GetMatrix4X4s();
            Matrix4x4[] spaceMatrixs = space.GetMatrix4X4s();

            // Create objects
            _mRs = new MeshRenderer[space.Datas.Length];
            for (int i = 0; i < _mRs.Length; i++)
            {
                _mRs[i] = Instantiate(_prefab, spaceMatrixs[i].GetPosition(), spaceMatrixs[i].rotation);
            }

            var offsets = GetOffset(modelMatrixs, spaceMatrixs);

            // Save to file
            DataConverter data = new DataConverter();
            data.SetMatrix4X4s(offsets.ToArray());
            string json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.streamingAssetsPath, "Results.json");
            File.WriteAllText(path, json);
        }


        private List<Matrix4x4> GetOffset(Matrix4x4[] models, Matrix4x4[] space)
        {
            List<Matrix4x4> results = new List<Matrix4x4>();

            // Get each posible offset
            for (int i = 0; i < space.Length; i++)
            {
                bool match = true;
                Matrix4x4 offset = space[i] * models[0].inverse;

                // Apply offset to each element of the model (except the first)
                for (int j = 1; j < models.Length && match; j++)
                {
                    Matrix4x4 shiftedModel = offset * models[j];
                    bool isContain = false;

                    // Check if space contains shiftedModel
                    for (int k = 0; k < space.Length; k++)
                    {
                        if (space[k].GetPosition() == shiftedModel.GetPosition() &&
                            space[k].rotation == shiftedModel.rotation &&
                            space[k].lossyScale == shiftedModel.lossyScale &&
                            !isContain)
                        {
                            isContain = true;
                            _mRs[k].material.color = Color.blue;
                        }
                    }

                    if (!isContain)
                    {
                        match = false;
                    }
                }

                if (match)
                {
                    results.Add(offset);
                }
            }
            return results;
        }
    }
}
